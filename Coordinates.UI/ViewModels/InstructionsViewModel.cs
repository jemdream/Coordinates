using Template10.Mvvm;

namespace Coordinates.UI.ViewModels
{
    public interface IInstructionsViewModel
    {
        string[] MachineInstructionTexts { get; }
        string[] MachineImageSources { get; }

        string[] MeasurementsInstructionTexts { get; }
        string[] MeasurementsImageSources { get; }
    }

    public class InstructionsViewModel : ViewModelBase, IInstructionsViewModel
    {
        public string[] MachineInstructionTexts { get; } = 
        {
            @"text opisujący włączanie maszyny - tej puszki, jakby była wyłączona",
            @"text żeby opisać jak przemieszczać się w osiach X Y Z i jak blokować oś"
        };
        public string[] MachineImageSources { get; } = 
        {
            @"ms-appx:///Assets/SplashScreen.png",
            @"ms-appx:///Assets/SplashScreen.png"
        };

        public string[] MeasurementsInstructionTexts { get; } =
        {
            @"text pomiar współrzędnościowy otworu/otworów",
            @"text pomiar współrzędnościowy płaszczyzn - równoległość/prostopadłość"
        };
        public string[] MeasurementsImageSources { get; } = 
        {
            @"ms-appx:///Assets/SplashScreen.png",
            @"ms-appx:///Assets/SplashScreen.png",
        };
    }
}
