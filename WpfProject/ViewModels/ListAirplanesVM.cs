using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.WebModels;
using WpfProject.Tools;
using WpfProject.WebModels;

namespace WpfProject.ViewModels
{
    public class ListAirplanesVM : BaseTools
    {
        public Airplane SelectedItem { get; set; }
        private List<AirplanesClass> airplanesClass;
        private List<Airplane> airplane;
       

        public List<Airplane> Airplane
        {
            get => airplane;
            set
            {
                airplane = value;

                Signal();
            }
        }



        public List<AirplanesClass> AirplanesClass
        {
            get => airplanesClass;
            set
            {
                airplanesClass = value;

                Signal();
            }
        }

        public CommandVM DeleteAirplane { get; set; }
       

        public ListAirplanesVM()
        {
            Task.Run(async () =>
            {
                var json = await HttpApi.Post("Airplanes", "ListAirplanes", null);
                Airplane = HttpApi.Deserialize<List<Airplane>>(json);

                var json2 = await HttpApi.Post("AirplanesClasses", "ListAirplanesClasses", null);
                AirplanesClass = HttpApi.Deserialize<List<AirplanesClass>>(json2);
            });

            DeleteAirplane = new CommandVM(async () =>
            {
                var json = await HttpApi.Post("Airplanes", "delete", SelectedItem.AirplanesId);

                Task.Run(async () =>
                {
                    var json = await HttpApi.Post("Airplanes", "ListAirplanes", null);
                    Airplane = HttpApi.Deserialize<List<Airplane>>(json);

                    var json2 = await HttpApi.Post("AirplanesClasses", "ListAirplanesClasses", null);
                    AirplanesClass = HttpApi.Deserialize<List<AirplanesClass>>(json);
                });
            });

        }
    }
}
