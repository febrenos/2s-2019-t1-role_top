using System.Collections.Generic;
using System.IO;
using RolêTopMVC.Models;

namespace RolêTopMVC.Repositories
{
    public class ShakeRepository
    {
        private const string PATH = "Database/Adicional.csv";

        public double ObterPrecoDe(string nomeShake)
        {
            var lista = ObterTodos();
            var preco = 0.0;
            foreach (var item in lista)
            {
                if(item.Nome.Equals(nomeShake))
                {
                    preco = item.Preco;
                    break;
                }
            }
            return preco;
        }

        public List<Adicional> ObterTodos()
        {
            List<Adicional> shakes = new List<Adicional>();

            var linhas = File.ReadAllLines(PATH);
            foreach(var linha in linhas)
            {
                Adicional s = new Adicional();
                var dados = linha.Split(";");
                s.Nome = dados[0];
                s.Preco = double.Parse(dados[1]);
                shakes.Add(s);
            }

            return shakes;
        }
    }
}