using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Zadanie1
{
    class MaszynaLosujaca
    {
        public List<string> napisy = new List<string>();

        public void DodajDoMaszyny(string napis)
        {
            napisy.Add(napis); 
        }

        public string WyjmijLosowyKupon()
        {
            var rnd = new Random();
            int index = rnd.Next(napisy.Count);
            string wylosowanyKupon = napisy[index];
            napisy.RemoveAt(index); // usuwanie kuponu z listy
            return wylosowanyKupon; 
        }

        public bool CzySaKupony()
        {
            if (napisy.Count == 0)
                return false;
            else
                return true; 
        }

        public string WypiszZawartoscMaszyny()
        {
            if (!CzySaKupony())
            {
                return "Maszyna jest pusta"; 
            }
            else
            {
                string zawartosc = "";
                foreach (var v in napisy)
                {
                    zawartosc += v + ", ";
                }
                return zawartosc;
            }
        }

    }
}
