using System;

namespace DIO.bank
{
    public class Conta
    {
        private string Nome { get; set; }
    	private TipoConta TipoConta { get; set; }
    	private double Saldo { get; set; }
    	private double Credito { get; set; }

		public Conta(TipoConta tipoConta, double saldo, double credito, string nome)
		{
			this.TipoConta = tipoConta;
			this.Saldo = saldo;
			this.Credito = credito;
			this.Nome = nome;
		}

		public void Depositar(double valorDeposito)
		{
			this.Saldo += valorDeposito;
            Console.WriteLine("\nResumo valores conta de {0}\nSaldo atual: R$ {1:0.00}\nCrédito: R$ {2:0.00}\nSaldo total: R$ {3:0.00}", this.Nome, this.Saldo, this.Credito, this.Saldo + this.Credito);
		}

        public bool Sacar(double valorSaque)
        {
            // Validação de saldo suficiente
            if (this.Saldo + this.Credito < valorSaque)
            {
                Console.WriteLine("Saldo insuficiente!");
                return false;
            }
            this.Saldo -= valorSaque;

            Console.WriteLine("\nResumo valores conta de {0}\nSaldo atual: R$ {1:0.00}\nCrédito: R$ {2:0.00}\nSaldo total: R$ {3:0.00}", this.Nome, this.Saldo, this.Credito, this.Saldo + this.Credito);
            // https://docs.microsoft.com/pt-br/dotnet/standard/base-types/composite-formatting

            return true;
        }

        public void Transferir(double valorTransferencia, Conta contaDestino)
        {
            if (this.Sacar(valorTransferencia))
                contaDestino.Depositar(valorTransferencia);
        }

        public double somaCredito()
        {
            return this.Credito;
        }

        public double somaSaldo()
        {
            return this.Saldo;
        }

        public double somaSaldoTotal()
        {
            return this.Credito + this.Saldo;
        }

        public string retornaTipoConta()
        {
            string strTipoConta;
            strTipoConta = Convert.ToString(this.TipoConta);
            return strTipoConta;
        }

        public override string ToString()
        {
            string strTipoConta = null;
            strTipoConta = Convert.ToString(this.TipoConta);

            if (strTipoConta == "PessoaFisica")
            {
                strTipoConta = "Pessoa Física";
            }
            else
            {
                strTipoConta = "Pessoa Jurídica";
            }

            string retorno = $"{this.Nome, -27}{strTipoConta, -20}{this.Saldo, 11:0.00}{this.Credito, 16:0.00}{this.Saldo + this.Credito, 16:0.00}";

            return retorno;
        }  	
    }
}
