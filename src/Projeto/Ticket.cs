using System.Net.Sockets;
using System.Text.Json;


namespace Projeto
{
    internal class Ticket
    {
        public Ticket()
        {

        }
        public Ticket(string idCliente)
        {
            this.Id = (RetornaLista().Count);
            this.Cliente = idCliente;
            Status = EStatusTicke.Aberto.ToString();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Setor { get; set; }
        public string Cliente { get; set; }
        public string Avaliacao { get; set; }
        public string Status { get; set; }
        public string Procedimento { get; set; }
        public enum EStatusTicke
        {
            Pendente = 0,
            Aberto = 1,
            Resolvido = 2
        }
        public enum ESetor
        {
            Suporte = 1,
            Financeiro = 2,
            Comercial = 3
        }
        public enum EAvaliacao
        {   bom = 1,
            ruim = 2
        }
        public static List<Ticket> RetornaLista()
        {
            string fileTicket = "arquivo-tickets";
            var conteudo = File.ReadAllText(fileTicket);
            var listaTickets = JsonSerializer.Deserialize<List<Ticket>>(conteudo)!;
            return listaTickets;
        }
        public void RetornaDados()
        {
            var cliente = RetornaCliente(); 
            ESetor setor = (ESetor)Setor;
            Console.Clear();
            Console.WriteLine($"Ticket: {this.Id}");
            Console.WriteLine($"Status: {this.Status}");
            Console.WriteLine($"Setor Responsavel: {setor}");
            Console.WriteLine($"Nome cliente: {cliente.Nome}");
            Console.WriteLine($"Email cliente: {cliente.Email}");
            Console.WriteLine($"Descriçao: {this.Descricao}");
            Console.ResetColor();

        }
        public Cliente? RetornaCliente()
        {
            string fileUsuario = "arquivo-usuarios";
            var conteudo = File.ReadAllText(fileUsuario);
            var listaUsuario = JsonSerializer.Deserialize<List<Cliente>>(conteudo);
            Cliente cliente = listaUsuario.FirstOrDefault( x => x.Id.Equals(Cliente))!;
            return cliente;
        }
        public void AlteraStatus(int status)
        {
            EStatusTicke eStatusTicke = (EStatusTicke)status;
            this.Status = eStatusTicke.ToString();
        }
        public void AdicionaAvaliacao(int avaliacao)
        {
            this.Status = EStatusTicke.Resolvido.ToString();
            EAvaliacao eAvaliacao = (EAvaliacao)avaliacao;
            this.Avaliacao = eAvaliacao.ToString();
        }
        public void FinalizaTicket()
        {
            RetornaDados();
            Console.WriteLine($"Procedimento: {this.Procedimento}");
            if (Avaliacao.Equals("ruim"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Avaliacao: {this.Avaliacao}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Avaliacao: {this.Avaliacao}");
                Console.ResetColor();
            }
            return;
        }

    }

}
