namespace RolêTopMVC.Models {
    public class Local : Produto {

        public Local () {

        }

        public Local (string nome, double preco) {
            this.Nome = nome;
            this.Preco = preco;
        }
    }
}