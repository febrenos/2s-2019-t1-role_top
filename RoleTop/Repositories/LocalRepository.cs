using System.Collections.Generic;
using System.IO;
using RoleTop.Models;

namespace RoleTop.Repositories {
    public class LocalRepository {
        private const string PATH = "Database/Hamburguer.csv";

        public double ObterPrecoDe(string nomeHamburguer)
        {

            var lista = ObterTodos();
            var preco = 0.0;
            foreach (var item in lista)
            {
                if(item.Nome.Equals(nomeHamburguer))
                {
                    preco = item.preco;
                    break;

                }
            }

            return preco;
        }
        public List<Local> ObterTodos () {
            List<Local> hamburgueres = new List<Local>();
            string[] linhas = File.ReadAllLines (PATH);
            foreach (var linha in linhas)
            {
                Local h = new Local();
                string[] dados = linha.Split(";"); //! Split: Quebra as linhas quando vê um ponto e virgula como mostra no caso do parametro
                h.Nome = dados[0];
                h.preco = double.Parse(dados[1]);
                hamburgueres.Add(h);


            }
            return hamburgueres;

        }
    }
}