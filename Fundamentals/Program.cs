using System;

namespace Fundamentals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Clear();

            // Using e Dispose
            using (var pagamento = new PagamentoBoleto(DateTime.Now))
            {
                Console.WriteLine("Processando o pagamento...");
            }

            /*
            // Upcast
            var pessoa = new Pessoa();

            pessoa = new PessoaFisica();
            pessoa = new PessoaJuridica();

            // Downcast
            var pessoaFisica = new PessoaFisica();

            pessoaFisica = (PessoaFisica)pessoa; 
            */

            // Comparando objetos
            var pessoaA = new Pessoa(1, "Kauan Hindlmayer");
            var pessoaB = new Pessoa(1, "Kauan Hindlmayer");

            Console.WriteLine(pessoaA == pessoaB); // false - Reference Types
            Console.WriteLine(pessoaA.Equals(pessoaB)); // true

            // Delegates => Anonymous Methods

            // Events
            var room = new Room(3);
            room.RoomSoldOutEvent += OnRoomSoldOut;
            room.ReserveSeat();
            room.ReserveSeat();
            room.ReserveSeat();
            room.ReserveSeat();

            // Lists
            var payments = new List<int>();
            payments.Add(1);
            payments.Add(2);
            payments.Add(3);

            foreach (var item in payments)
	        {
                Console.WriteLine(item.ToString());
	        }

            var paidPayments = new List<int>();
            paidPayments.AddRange(payments);

            // LINQ - Language INtegrated Query
            var payment = payments.Where(x => x == 3);
            Console.WriteLine(payment);

            payments.AsEnumerable(); // Converte uma lista para um Enumerable
        }
        
        static void OnRoomSoldOut(object sender, EventArgs e)
        {
            Console.WriteLine("Sala lotada!");
        }
    }

    public class Pessoa : IEquatable<Pessoa>
    {
        public Pessoa(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public bool Equals(Pessoa other)
        {
            return Id == other.Id;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class PessoaFisica
    {
        public int CPF { get; set; }
    }

    public class PessoaJuridica
    {
        public int CNPJ { get; set; }
    }

    struct PessoaStruct // Tipo de Valor (valor dos dados)
    {
        string Nome;
    }

    /*  private (disponível apenas dentro da classe),
        protected (disponível apenas dentro da classe e das classes que herdeiras), 
        internal (disponível dentro do mesmo namespace) e 
        public (disponível em qualquer lugar) */

    // Classes abstratas não podem ser instanciadas
    abstract class Pagamento : IDisposable // Tipo de Referência (Endereço dos dados)
    {
        public Pagamento() { }
        public Pagamento(DateTime vencimento) // Método Construtor
        {
            Console.WriteLine("Iniciando o pagamento...");
            DataPagamento = DateTime.Now;
        }

        // Propriedades
        public DateTime Vencimento { get; set; }
        private DateTime _dataPagamento;
        public DateTime DataPagamento
        {
            get
            {
                // Console.WriteLine("Lendo o valor");
                return _dataPagamento;
            }
            set
            {
                // Console.WriteLine("Atribuindo o valor");
                _dataPagamento = value;
            }
        }

        // Métodos
        public virtual void Pagar(string numero)
        {
            Console.WriteLine("Pagar");
        } 
        
        public void Pagar(string numero, DateTime vencimento) { } // nomes iguais, mas assinaturas diferentes
        
        public void Dispose()
        {
            Console.WriteLine("Finalizando o pagamento..."); 
        }
    }

    class PagamentoBoleto : Pagamento
    {
        public PagamentoBoleto(DateTime vencimento): base(vencimento) { }
        public string NumeroBoleto;
        public override void Pagar(string numero)
        {
            base.Pagar(numero);
            // Regra do Boleto
        }
    }

    class PagamentoCartaoCredito : Pagamento
    {
        public string Numero;
        public override void Pagar(string numero)
        {
            // Regra do Cartão de Crédito
        }
    }

    public static class Settings //  Classe estática, uma única alocação na memória
    { 
        public static string API_URL { get; set; }
    }

    public interface IPagamento
    {

    }

    public class Room
    {
        public Room (int seats)
	    {
            Seats = seats;
            seatsInUse = 0;
	    }

        private int seatsInUse = 0;
        public int Seats { get; set; }

        public void ReserveSeat()
        {
            seatsInUse++;
            if (seatsInUse >= Seats)
            {
                OnRoomSoldOut(EventArgs.Empty);
            }
            else
            {
                Console.WriteLine("Assento reservado");
            }
        }
    
        public event EventHandler RoomSoldOutEvent;
        
        protected virtual void OnRoomSoldOut(EventArgs e)
        {
            EventHandler handler = RoomSoldOutEvent;
            handler?.Invoke(this, e);
        }
    }
}