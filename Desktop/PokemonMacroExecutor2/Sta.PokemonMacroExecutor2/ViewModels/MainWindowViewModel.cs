using Prism.Mvvm;
using System.Windows;

namespace Sta.PokemonMacroExecutor2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string m_title = "Pokémon Macro Executor 2";
        public string Title
        {
            get { return m_title; }
            set { SetProperty(ref m_title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
