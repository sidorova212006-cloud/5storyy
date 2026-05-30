namespace plug.Models
{
    public class WallInputBuilder
    {
        private double _length;
        private double _width;
        private double _height;
        private double _openingsArea;

        public WallInputBuilder SetLength(double length)
        {
            _length = length;
            return this;
        }

        public WallInputBuilder SetWidth(double width)
        {
            _width = width;
            return this;
        }

        public WallInputBuilder SetHeight(double height)
        {
            _height = height;
            return this;
        }

        public WallInputBuilder SetOpenings(double openingsArea)
        {
            _openingsArea = openingsArea;
            return this;
        }

        public WallInput Build()
        {
            return new WallInput
            {
                Length = _length,
                Width = _width,
                Height = _height,
                OpeningsArea = _openingsArea
            };
        }
    }
}
