using System;
using System.Collections.Generic;
using System.IO;
using McBonaldsMVC.Models;

namespace McBonaldsMVC.Repositories {
    public class PedidoRepository : RepositoryBase {
        private const string PATH = "Database/Pedido.csv";

        public PedidoRepository () {
            if (!File.Exists (PATH)) {
                File.Create (PATH).Close ();
            }
        }

        public bool Inserir (Pedido pedido) {
            var quantidadePedidos = File.ReadAllLines (PATH).Length; //! ReadAlllines le o arquivo todo e devolve as linhas
            pedido.Id = (ulong) ++quantidadePedidos;
            var linha = new string[] { PrepararPedidoCSV (pedido) };

            File.AppendAllLines (PATH, linha);

            return true;

        }

        public List<Pedido> ObterTodosPorCliente (string emailCliente)

        {
            var pedidos = ObterTodos ();
            List<Pedido> pedidosCliente = new List<Pedido> ();
            foreach (var pedido in pedidos) {
                if (pedido.Cliente.Email.Equals (emailCliente)) {
                    pedidosCliente.Add (pedido);
                }
            }
            return pedidosCliente;
        }
        public List<Pedido> ObterTodos () {
            var linhas = File.ReadAllLines (PATH);
            List<Pedido> pedidos = new List<Pedido> ();

            foreach (var linha in linhas) {
                Pedido pedido = new Pedido ();
                pedido.Hamburguer = new Hamburguer ();
                pedido.Shake = new Shake ();
                pedido.Cliente = new Cliente ();
                pedido.Id = ulong.Parse(ExtrairValorDoCampo("pedido_id",linha));
                pedido.Status = uint.Parse(ExtrairValorDoCampo("status_pedido", linha));
                pedido.Cliente.Nome = ExtrairValorDoCampo ("cliente_nome", linha);
                pedido.Cliente.Endereco = ExtrairValorDoCampo ("cliente_endereco", linha);
                pedido.Cliente.Email = ExtrairValorDoCampo ("cliente_email", linha);
                pedido.Cliente.Telefone = ExtrairValorDoCampo ("cliente_telefone", linha);
                pedido.Hamburguer.Nome = ExtrairValorDoCampo ("hamburguer_nome", linha);
                pedido.Hamburguer.preco = double.Parse (ExtrairValorDoCampo ("hamburguer_preco", linha));
                pedido.Shake.Nome = ExtrairValorDoCampo ("shake_nome", linha);
                pedido.Shake.preco = double.Parse (ExtrairValorDoCampo ("shake_preco", linha));
                pedido.PrecoTotal = double.Parse (ExtrairValorDoCampo ("preco_total", linha));
                pedido.DataDoPedido = DateTime.Parse (ExtrairValorDoCampo ("data_pedido", linha));

                pedidos.Add (pedido);
            }
            return pedidos;
        }

        public Pedido ObterPor(ulong id)
        {
            var pedidosTotais = ObterTodos();
            foreach (var pedido in pedidosTotais)
            {
                if(id.Equals(pedido.Id))
                {
                    return pedido; //se achar retorna o obj
                }
            }
            return null; //se nao achar retorna nulo
        }

        public bool Atualizar(Pedido pedido) //Métodod para atualizar
        {
            var pedidosTotais = File.ReadAllLines(PATH);
            var pedidoCSV = PrepararPedidoCSV(pedido);
            var linhaPedido = -1; // pois no csv começa e ler do 0
            var resultado = false;

            for (int i = 0; i < pedidosTotais.Length; i++)
            {
                var idConvertido = ulong.Parse(ExtrairValorDoCampo("id",pedidosTotais[i])); // pega o id de todods o pedidos e converte para o q a gnt atualizou 
                if(pedido.Id.Equals(idConvertido))
                {
                    linhaPedido = i;
                    resultado = true;
                    break;
                }
            }

            if(resultado) // da para usar if pois é booleano
            {
                pedidosTotais[linhaPedido] = pedidoCSV;
                File.WriteAllLines(PATH, pedidosTotais);
            }

            return resultado;
        }

        private string PrepararPedidoCSV (Pedido pedido) {
            Cliente cliente = pedido.Cliente;
            Hamburguer hamburguer = pedido.Hamburguer;
            Shake shake = pedido.Shake;

            return $"pedido_id={pedido.Id};status_pedido={pedido.Status};cliente_nome={cliente.Nome};cliente_email={cliente.Email};cliente_endereco={cliente.Endereco};cliente_telefone={cliente.Telefone};hamburguer_preco={hamburguer.preco};hamburguer_nome={hamburguer.Nome};shake_nome={shake.Nome};shake_preco={shake.preco};data_pedido={pedido.DataDoPedido};preco_total={pedido.PrecoTotal}"; //substituição do bloco
        }
    }
}