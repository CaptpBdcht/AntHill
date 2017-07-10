using System;
using System.Diagnostics.Contracts;

namespace Engine.Map
{
    [Serializable]
    public abstract class Board : ICloneable
    {
        private readonly int _size;
        public int Size
        {
            get { return _size; }
        }

        protected readonly Zone[,] _zones;

        public Zone[,] Zones
        {
            get { return _zones; }
        }

        protected Board(int size, IZoneFactory factory)
        {
            _size = size;
            _zones = new Zone[_size, _size];
            BoardMetadata.BoardSize = _size;

            InitBoard(factory);
        }

        protected Board(int size, Zone[,] zones)
        {
            _size = size;
            _zones = zones;
            BoardMetadata.BoardSize = _size;
        }

        protected abstract void InitBoard(IZoneFactory factory);
        
        public Zone Get(Location location)
        {
            Contract.Assert(0 <= location.Latitude && location.Latitude < Size);
            Contract.Assert(0 <= location.Longitude && location.Longitude < Size);

            return _zones[location.Latitude, location.Longitude];
        }

        public object Clone()
        {
            Zone[,] zones = new Zone[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    zones[i, j] = (Zone)_zones[i, j].Clone();
                }
            }

            return Clone(zones);
        }

        protected abstract object Clone(Zone[,] clonedZones);
    }
}