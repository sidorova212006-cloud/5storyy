namespace plug.ViewModels
{
    public class MainViewModel
    {
        public LinoleumViewModel Linoleum { get; } = new LinoleumViewModel();
        public WallViewModel Wall { get; } = new WallViewModel();
        public ConcreteViewModel Concrete { get; } = new ConcreteViewModel();
        public TileViewModel Tile { get; } = new TileViewModel();
        public RafterViewModel Rafter { get; } = new RafterViewModel();
    }
}


