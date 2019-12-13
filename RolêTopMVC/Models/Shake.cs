namespace RolêTopMVC.Models
{
    public class Adicional : Produto
    {
        public Adicional () {

        }

        public Adicional (string nome, double preco) {
            this.Nome = nome;
            this.Preco = preco;
        }
    }
}