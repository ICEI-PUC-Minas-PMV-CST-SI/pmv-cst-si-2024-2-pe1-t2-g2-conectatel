using System.Text.Json;


namespace Projeto
{
    internal class Menu
    {
        string fileTicket = "arquivo-tickets";
        string fileUsuario = "arquivo-usuarios";
        public void MenuLogin()
        {
            Console.Clear();
            Cliente cliente = new Cliente();
            Console.WriteLine("Tela de login");
            Console.WriteLine("Login: admin - admin");
            Console.Write("\nLogin:");
            var login = Console.ReadLine()!;
            Console.Write("\nSenha:");
            var senha = Console.ReadLine()!;
            cliente.Login.Add(login,senha);
            //CriarCliente(cliente);
            cliente = VerificaCliente(login,senha);
            if (cliente is null)
            {
                Console.WriteLine("Login ou senha inválido");
                Console.ReadKey();
                MenuLogin();    
            }
            else
                MenuValidacao(cliente);

        }
        void MenuValidacao(Cliente cliente)
        {
            Console.Clear();
            Console.Write("Digite nome:");
            cliente.Nome = Console.ReadLine()!;
            Console.Write("\nDigite email:");
            cliente.Email = Console.ReadLine()!;
            CriarCliente(cliente);
            Ticket ticket = new Ticket(cliente.Id);
            MenuAbrirChamado(ticket);
        }
        void MenuAbrirChamado(Ticket ticket)
        {
            Console.Clear();
            Console.WriteLine("Tela de abertura de chamado");
            Console.WriteLine("1- Suporte");
            Console.WriteLine("2- Financeiro");
            Console.WriteLine("3- Comercial");
            Console.Write("Escolha atendimento:");
            ticket.Setor = Escolha(1, 3);
            Console.WriteLine("Descricao incidente: ");
            ticket.Descricao = Console.ReadLine()!;
            MenuSolucao(ticket);
        }
        void MenuSolucao(Ticket ticket)
        {
            Console.Clear();
            Console.WriteLine("Tela solucao");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ticket criado");
            ticket.RetornaDados();
            Console.ResetColor();
            Console.ReadLine();
            Console.WriteLine("Foi solucionado pelo operador?");
            Console.WriteLine("1- Sim");
            Console.Write("2- Nao\n");
            var escolha = Escolha(1, 2);
            if (escolha == 1)
                MenuRegistro(ticket);
            if (escolha == 2)
                MenuAlteraSetor(ticket);
        }
        void MenuAlteraSetor(Ticket ticket)
        {
            Console.Clear();
            Console.WriteLine("Tela troca setor");
            Console.ForegroundColor = ConsoleColor.Blue;
            ticket.AlteraStatus(0);
            var setor = ticket.Setor > 1 ? 1 : 2;
            ticket.Setor = setor;
            ticket.RetornaDados();
            Console.ReadLine();
            MenuRegistro(ticket);
        }
        void MenuRegistro(Ticket ticket)
        {
            Console.Clear();
            Console.WriteLine("Tela registro procedimento");
            Console.Write("\nRegistro:");
            ticket.Procedimento = Console.ReadLine()!;
            Console.WriteLine("Avaliacao:");
            Console.WriteLine("1- bom");
            Console.WriteLine("2- ruim");
            ticket.AdicionaAvaliacao(Escolha(1, 2));
            CriarTicket(ticket);
            ticket.FinalizaTicket();
            Console.ReadKey();
            MenuLogin();
        }

        void CriarCliente(Cliente cliente)
        {
            if (File.Exists(fileUsuario))
            {
                var conteudo = File.ReadAllText(fileUsuario);
                var listaUsuario = JsonSerializer.Deserialize<List<Cliente>>(conteudo);
                if (listaUsuario.Any(x => x.Id.Equals(cliente.Id)))
                {
                    int index = listaUsuario.FindIndex(x => x.Id.Equals(cliente.Id));
                    listaUsuario[index] = cliente;
                }
                listaUsuario.Add(cliente);
                var conteudoSerializado = JsonSerializer.Serialize<List<Cliente>>(listaUsuario);
                File.WriteAllText(fileUsuario, conteudoSerializado);
                return;
            }
        }
        Cliente? VerificaCliente(string usuario, string senha)
        {
            var conteudo = File.ReadAllText(fileUsuario);
            var listaUsuario = JsonSerializer.Deserialize<List<Cliente>>(conteudo)!;
            if (listaUsuario.Any(x => x.Login.ContainsKey(usuario)) == true)
            {
                var cliente =listaUsuario.FirstOrDefault(x => x.Login.ContainsKey(usuario.Trim()));
                var retorno = cliente.Login[usuario].Equals(senha.Trim()) ? cliente : null;
                return  retorno;
            }
            return null;
        }
        int Escolha(int inicio, int final)
        {
            int escolhaNum = -1;
            bool consulta = false;
            do
            {
                var escolha = Console.ReadLine();
                consulta = int.TryParse(escolha, out escolhaNum);
            } while (!(consulta) || escolhaNum < inicio || escolhaNum > final);
            return escolhaNum;

        }
        void CriarTicket(Ticket ticket)
        {
            if (File.Exists(fileTicket))
            {
                var conteudo = File.ReadAllText(fileTicket);
                var listaTicket = JsonSerializer.Deserialize<List<Ticket>>(conteudo);
                listaTicket.Add(ticket);
                listaTicket.Distinct();
                var conteudoSerializado = JsonSerializer.Serialize<List<Ticket>>(listaTicket);
                File.WriteAllText(fileTicket, conteudoSerializado);
                return;
            }
        }
    }
}
