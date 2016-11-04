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
            @"Po podłączeniu maszyny do zasilania należy sprawdzić, czy przełącznik widoczny na zdjęciu poniżej jest w pozycji ON.
Oprócz tego należy sprawdzić czy do komputera podłączony jest przewód USB.",
            @"Aby zmienić pozycję sensora należy obracać pokrętło w odpowiedniej osi. Są one opisane na obudowie.
Pokrętłami należy obracać tak, aby kulka sensora stykowego dotknęła przedmiotu mierzonego.
W celu zablokowania ruchu osi trzeba dokręcić śrubę przedstawioną na poniższym zdjęciu."
        };
        public string[] MachineImageSources { get; } =
        {
            @"ms-appx:///Assets/SplashScreen.png",
            @"ms-appx:///Assets/SplashScreen.png"
        };

        public string[] MeasurementsInstructionTexts { get; } =
        {
            @"Wynik pomiaru otworu to średnica oraz współrzędne środka.
W przypadku wybrania dwóch otworów elementów dochodzi jeszcze odległość pomiędzy ich środkami.
Minimalna liczba punktów pomiarowych to 5 dla każdego elementu.",
            @"Wynik pomiaru płaszczyzny to równanie opisujące ją w układzie XYZ.
Zestawiając dwie płaszczyzny można obliczyć kąt między nimi.
W przypadku prostopadłości, będą to dwie płaszczyzny w różnych osiach, a równoległości w tych samych.
Minimalna liczba punktów pomiarowych to 5 dla każdego elementu."
        };
        public string[] MeasurementsImageSources { get; } =
        {
            @"ms-appx:///Assets/otwory.png",
            @"ms-appx:///Assets/plaszczyzny.png",
        };
    }
}
