using System.ComponentModel;

namespace VetClinic.DAL
{
    public static class HastaStore
    {
        public static BindingList<Hasta> Hastalar { get; } = new BindingList<Hasta>();
    }
}
