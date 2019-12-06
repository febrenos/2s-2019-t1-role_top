using System;
using System.Collections.Generic;
using System.IO;
using RoleTop.Models;

namespace RoleTop.Repositories {
    public class ReserveRepository : RepositoryBase {
        private const string PATH = "Database/Pedido.csv";

        public ReserveRepository () {
            if (!File.Exists (PATH)) {
                File.Create (PATH).Close ();
            }
        }

        public bool Inserir (Reserve pedido) {
            var quantidadePedidos = File.ReadAllLines (PATH).Length; //! ReadAlllines le o arquivo todo e devolve as linhas
            pedido.Id = (ulong) ++quantidadePedidos;
            var linha = new string[] { PrepararPedidoCSV (pedido) };

            File.AppendAllLines (PATH, linha);

            return true;

        }

        public List<Reserve> ObterTodosPorCliente (string emailCliente)

        {
            var pedidos = ObterTodos ();
            List<Reserve> pedidosCliente = new List<Reserve> ();
            foreach (var pedido in pedidos) {
                if (pedido.Cliente.Email.Equals (emailCliente)) {
                    pedidosCliente.Add (pedido);
                }
            }
            return pedidosCliente;
        }
        public List<Reserve> ObterTodos () {
            var linhas = File.ReadAllLines (PATH);
            List<Reserve> pedidos = new List<Reserve> ();

            foreach (var linha in linhas) {
                Reserve pedido = new Reserve ();
                pedido.Local = new Local ();
                pedido.Cliente = new Cliente ();
                pedido.Id = ulong.Parse(ExtrairValorDoCampo("pedido_id",linha));
                pedido.Status = uint.Parse(ExtrairValorDoCampo("status_pedido", linha));
                pedido.Cliente.Nome = ExtrairValorDoCampo ("cliente_nome", linha);
                pedido.Cliente.Endereco = ExtrairValorDoCampo ("cliente_endereco", linha);
                pedido.Cliente.Email = ExtrairValorDoCampo ("cliente_email", linha);
                pedido.Cliente.Telefone = ExtrairValorDoCampo ("cliente_telefone", linha);
                pedido.Local.Nome = ExtrairValorDoCampo ("hamburguer_nome", linha);
                pedido.Local.preco = double.Parse (ExtrairValorDoCampo ("hamburguer_preco", linha));
                pedido.PrecoTotal = double.Parse (ExtrairValorDoCampo ("preco_total", linha));
                pedido.DataDoPedido = DateTime.Parse (ExtrairValorDoCampo ("data_pedido", linha));

                pedidos.Add (pedido);
            }
            return pedidos;
        }

        public Reserve ObterPor(ulong id)
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

        public bool Atualizar(Reserve pedido) //Métodod para atualizar
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

        private string PrepararPedidoCSV (Reserve reserve) {
            Cliente cliente = reserve.Cliente;
            Local local = reserve.Local;

            return $"pedido_id={reserve.Id};status_pedido={reserve.Status};cliente_nome={cliente.Nome};cliente_email={cliente.Email};cliente_endereco={cliente.Endereco};cliente_telefone={cliente.Telefone};hamburguer_preco={Local.preco};hamburguer_nome={Local.Nome};data_pedido={Reserve.DataDoPedido};preco_total={Reserve.PrecoTotal}"; //substituição do bloco
        }
    }
}