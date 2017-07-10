using System;
using Engine.Map;
using Anthill.Zones;
using Engine;
using System.Collections.Generic;

namespace Anthill
{
    [Serializable]
    public class Valhalla : Board, ICloneable
    {
        public Valhalla(int size, ZoneFactory factory) : base(size, factory) {}

        private Valhalla(int size, Zone[,] zones): base (size, zones) {}

        protected override object Clone(Zone[,] clonedZones)
        {
            return new Valhalla(Size, clonedZones);
        }

        protected override void InitBoard(IZoneFactory factory)
        {
            var zoneFactory = (ZoneFactory) factory;
            
            InitPlainWorld(zoneFactory.MakeGround);
            InitForest(zoneFactory.MakeForest);
            InitLava(zoneFactory.MakeLava);
        }

        private void InitPlainWorld(Func<Zone> groundMaker)
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    _zones[i, j] = groundMaker();
        }

        private void InitLava(Func<Zone> lavaMaker)
        {
            foreach (var location in LinearPointGenerate(0.3, BoardMetadata.BoardSize / 3))
                _zones[ location.Latitude, location.Longitude ] = lavaMaker();

            foreach (var location in PolynomialPointGenerate(0.1, 0, -6))
                _zones[location.Latitude, location.Longitude] = lavaMaker();
        }

        private void InitForest(Func<Zone> forestMaker)
        {
            int size = BoardMetadata.BoardSize * BoardMetadata.BoardSize;
            int trees = BoardMetadata.Random.Next(
                size / 5,
                (3 * size) / 2
            );

            foreach (var location in CloudPointGenerate(trees, BoardMetadata.BoardSize, BoardMetadata.BoardSize, new int[] { 0, 0 }))
                _zones[location.Latitude, location.Longitude] = forestMaker();
        }

        private IEnumerable<Location> LinearPointGenerate(double a = 1, double b = 0)
        {
            double coef = a; // ax + b, coef is a
            double total = b; // b = 0

            const double step = 0.125;
            double stop = Size - 1 + 0.01;

            for (double i = 0; i < stop; i += step)
            {
                int x = (int) Math.Round(i, 0, MidpointRounding.AwayFromZero);
                int y = (int) Math.Round(total, 0, MidpointRounding.AwayFromZero);

                if (0 <= y && y < Size && !(_zones[x, y] is Lava))
                    yield return new Location(x, y);

                total += (coef * step);
            }
        }

        private IEnumerable<Location> PolynomialPointGenerate(double a = 1, double b = 0, double c = 0)
        {
            double sqrCoef = a; // ax² + bx + c, sqrCoef is a
            double coef = b; // coef is b
            double total = c; // c

            const double step = 0.125;
            double stop = Size - 1 + 0.001;

            for (double i = 0; i < stop; i += step)
            {
                int x = (int) Math.Round(i, 0, MidpointRounding.AwayFromZero);
                int y = (int) Math.Round(total, 0, MidpointRounding.AwayFromZero);

                if (0 <= y && y < Size && !(_zones[x, y] is Lava))
                    yield return new Location(x, y);

                total = c + (coef * i) + (sqrCoef * i * i);
            }
        }

        private IEnumerable<Location> CloudPointGenerate(int points, int xInterval, int yInterval, int[] firstPoint)
        {
            while (points > (xInterval * yInterval))
                points = (xInterval * yInterval) / 2;

            for (int i = 0; i < points; i++)
            {
                int x = BoardMetadata.Random.Next(0, xInterval) + firstPoint[0];
                int y = BoardMetadata.Random.Next(0, yInterval) + firstPoint[1];

                if (_zones[x, y] is Forest)
                    i--;
                else
                    yield return new Location(x, y);
            }
        }
    }
}
