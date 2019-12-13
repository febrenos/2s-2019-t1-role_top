using System;
using System.Collections.Generic;
using System.IO;
using RolêTopMVC.Models;

namespace RolêTopMVC.Repositories {
    public class PedidoRepository  : RepositoryBase{
        private const string PATH = "Database/Pedido.csv";

        public PedidoRepository () {
            if (!File.Exists (PATH)) {

                File.Create (PATH).Close ();
            }
        }
        public bool Inserir (Pedido pedido) {
            var quantidadePedidos = File.ReadAllLines(PATH).Length;
            pedido.Id = (ulong) ++quantidadePedidos;
            var linha = new string[] { PrepararPedidoCSV (pedido) };
            File.AppendAllLines (PATH, linha);

            return true;
        }

        public List<Pedido> ObterTodosPorCliente(string emailCliente)
        {
        List<Pedido> pedidosCliente = new List<Pedido>();
            var pedidos = ObterTodos();
            foreach (var pedido in pedidos)
            {
                if(pedido.Cliente.Email.Equals(emailCliente))
                {
                    pedidosCliente.Add(pedido);
                }
            }
            return pedidosCliente;
        }

        public List<Pedido> ObterTodos()
        {
            var linhas = File.ReadAllLines(PATH);
            List<Pedido> pedidos = new List<Pedido>();

            foreach (var linha in linhas)
            {
                Pedido pedido = new Pedido();
                
                pedido.Id = ulong.Parse(ExtrairValorDoCampo("id", linha));
                pedido.Status = uint.Parse(ExtrairValorDoCampo("status_pedido", linha));
                pedido.Cliente.Nome = ExtrairValorDoCampo("cliente_nome", linha);
                pedido.Cliente.Endereco = ExtrairValorDoCampo("cliente_endereco", linha);
                pedido.Cliente.Telefone = ExtrairValorDoCampo("cliente_telefone", linha);
                pedido.Cliente.Email = ExtrairValorDoCampo("cliente_email", linha);
                pedido.Local.Nome = ExtrairValorDoCampo("hamburguer_nome", linha);
                pedido.Local.Preco = double.Parse(ExtrairValorDoCampo("hamburguer_preco", linha));
                pedido.Adicional.Nome = ExtrairValorDoCampo("shake_nome", linha);
                pedido.Adicional.Preco = double.Parse(ExtrairValorDoCampo("shake_preco", linha));
                pedido.PrecoTotal = double.Parse(ExtrairValorDoCampo("preco_total", linha));
                pedido.DataDoPedido = DateTime.Parse(ExtrairValorDoCampo("data_pedido", linha));

                pedidos.Add(pedido);
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
                    return pedido;
                }
            }
            return null;
        }

        public bool Atualizar(Pedido pedido)
        {
            var pedidosTotais = File.ReadAllLines(PATH);
            var pedidoCSV =  PrepararPedidoCSV(pedido);
            var linhaPedido = -1;
            var resultado = false;

            for (int i = 0; i < pedidosTotais.Length; i++)
            {
                var idConvertido =  ulong.Parse(ExtrairValorDoCampo("id", pedidosTotais[i]));
                if(pedido.Id.Equals(idConvertido))
                {
                    linhaPedido = i;
                    resultado = true;
                    break;
                }
            }

            if(resultado)
            {
                pedidosTotais[linhaPedido] = pedidoCSV;
                File.WriteAllLines(PATH, pedidosTotais);
            }

            return resultado;
        }


        private string PrepararPedidoCSV (Pedido pedido) {

            Cliente c = pedido.Cliente;
            Local h = pedido.Local;
            Adicional s = pedido.Adicional;
            return $"id={pedido.Id};status_pedido={pedido.Status};cliente_nome={c.Nome};cliente_endereco={c.Endereco};cliente_telefone={c.Telefone};cliente_email={c.Email};hamburguer_nome={h.Nome};hamburguer_preco={h.Preco};shake_nome={s.Nome};shake_preco={s.Preco};data_pedido={pedido.DataDoPedido};preco_total={pedido.PrecoTotal}";
        }
    }
}