using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Engine.Entity;
using Engine.Map;
using Anthill;

namespace fourmilliereALIHM
{
    public static class WindowUtils
    {
        public static Image FindZoneImage(Zone zone)
        {
            return FindImage(zone.Name);
        }

        public static Image FindEntityImage(Entity entity, Team team)
        {
            if (entity is Ant) return FindAntImage(entity.Name, team.Name[0]);
            return FindImage(entity.Name);
        }

        public static Image FindImage(string name)
        {
            Image image = new Image();
            string imageName = "images/" + name.ToLower() + ".png";
            Uri uri = new Uri(imageName, UriKind.Relative);
            image.Source = new BitmapImage(uri);

            return image;
        }

        public static Image FindAntImage(string entityName, char teamNum)
        {
            Image image = new Image();
            string imageName = "images/" + entityName.ToLower() + teamNum + ".png";
            Uri uri = new Uri(imageName, UriKind.Relative);
            image.Source = new BitmapImage(uri);

            return image;
        }
    }
}
