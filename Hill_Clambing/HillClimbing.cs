using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hill_climbing
{
    class HillClimbing
    {
        public List<Double> najlepszy = new List<double>();
        public List<Double> VMs = new List<double>();


        public String LosowyOsobnik(int a, int b, int decimals, double dok)
        {
            Konwersja konwersja = new Konwersja();
            Random rand = new Random();
            return konwersja.ToBin(a, b, konwersja.ToInt(a, b, Math.Round(rand.NextDouble() * (b - a) + a, decimals), dok), dok);
        }


        public List<String> TworzenieMutacje(String xbin, int a, int b, double dok)
        {
            Konwersja konwersja = new Konwersja();
            List<String> zmutowany = new List<string>();
            for (int i = 0; i < xbin.Length; i++)
            {
                zmutowany.Add(Mutacja(xbin, i));
            }
            return zmutowany;
        }
        private String Mutacja(String xbin, int pozycja)
        {
            if (xbin.ElementAt(pozycja) == '1')
            {
                StringBuilder sb = new StringBuilder(xbin);
                sb[pozycja] = '0';
                xbin = sb.ToString();
            }
            else
            {
                StringBuilder sb = new StringBuilder(xbin);
                sb[pozycja] = '1';
                xbin = sb.ToString();
            }
            return xbin;
        }
        public String Iteracja(int a, int b, int decimals, double dok)
        {

            Konwersja konwersja = new Konwersja();
            var vc = LosowyOsobnik(a, b, decimals, dok);
            var local = false;
            var fvc = konwersja.Feval(konwersja.ToReal(a, b, konwersja.ToIntFromBin(vc),dok, decimals));

            while(!local)
            {
                var mozliwaMutacja = TworzenieMutacje(vc, a, b, dok);
                List<Double> fevals = new List<double>();
                for (int i = 0; i < mozliwaMutacja.Count; i++) fevals.Add(konwersja.Feval(konwersja.ToReal(a, b, konwersja.ToIntFromBin(mozliwaMutacja[i]), dok, decimals)));
                var vm = fevals[0]; var index = 0;
                for (int i = 1; i < fevals.Count; i++)
                {
                    if (vm < fevals[i])
                    {
                        vm = fevals[i];
                        index = i;
                    }

                }
                VMs.Add(vm);
                if (fvc < vm)
                {
                    vc = mozliwaMutacja[index];
                    fvc = vm;
                }
                else
                {
                    local = true;
                }
                if (najlepszy.Count != 0)
                    if (najlepszy.Last() < fvc) najlepszy.Add(fvc);
                    else najlepszy.Add(najlepszy.Last());
                else najlepszy.Add(fvc);

            }
            return vc;
        }
        public String Wyzazanie(int a, int b, int decimals, double dok, int T)
        {
            najlepszy = new List<double>();
            VMs = new List<double>();
            String best = "";
            for (int i = 0; i < T; i++)
            {
                best = Iteracja(a, b, decimals, dok);
            }
            return best;
        }

    }
}
