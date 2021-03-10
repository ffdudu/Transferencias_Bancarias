using System;
using System.Collections.Generic;

namespace DIO.bank
{
    class Program
    {
        public static List<Conta> listContas = new List<Conta>();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                try
                {
                    switch (opcaoUsuario)
                    {
                        case "1":
                            ListarContas();
                            break;
                        case "2":
                            InserirConta();
                            break;
                        case "3":
                            Transferir();
                            break;
                        case "4":
                            Sacar();
                            break;
                        case "5":
                            Depositar();
                            break;
                        case "C":
                            Console.Clear();
                            break;
                    }
                }
                catch
                {
                    continue;
                }
                opcaoUsuario = ObterOpcaoUsuario();
            }
            
            Console.WriteLine("\nObrigado por utilizar nossos serviços.\n");          
        }

        private static void Transferir()
        {
            if (listContas.Count > 0)
            {
                string ValorInserido = null;
                int indiceContaOrigem;
                int indiceContaDestino;
                double valorTransferencia;

                Console.Write("Digite o número da conta de origem (ou X para Sair): ");
                ValorInserido = Console.ReadLine().ToUpper();
                if (ValorInserido != "X")
                    indiceContaOrigem = int.Parse(ValorInserido);
                else
                    return;

                Console.Write("Digite o número da conta de destino (ou X para Sair): ");
                ValorInserido = Console.ReadLine().ToUpper();
                if (ValorInserido != "X")
                    indiceContaDestino = int.Parse(ValorInserido);
                else
                    return;

                Console.Write("Digite o valor a ser transferido (ou X para Sair): ");
                ValorInserido = Console.ReadLine().ToUpper().Replace(",",".");
                if (ValorInserido != "X")
                {
                    if(double.Parse(ValorInserido) <= 0)
                        valorTransferencia = double.Parse(ValorInserido + "a");
                    else
                        valorTransferencia = double.Parse(ValorInserido);
                }
                else
                    return;

                listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino]);
                return;
            }

            RetornaSemConta();
        }

        private static void Depositar()
        {   
            if (listContas.Count > 0)
            {
                string ValorInserido = null;
                int indiceConta;
                double valorDeposito;


                Console.Write("Digite o número da conta (ou X para Sair): ");
                ValorInserido = Console.ReadLine().ToUpper();
                if (ValorInserido != "X")
                    indiceConta = int.Parse(ValorInserido);
                else
                    return;

                Console.Write("Digite o valor a ser depositado (ou X para Sair): ");              
                ValorInserido = Console.ReadLine().ToUpper().Replace(",",".");
                if (ValorInserido != "X")
                {
                    if(double.Parse(ValorInserido) <= 0)
                        valorDeposito = double.Parse(ValorInserido + "a");
                    else
                        valorDeposito = double.Parse(ValorInserido);
                }
                else
                    return;

                listContas[indiceConta].Depositar(valorDeposito);
                return;

            }

            RetornaSemConta();
        }

        private static void Sacar()
        {
            if (listContas.Count > 0)
            {

                string ValorInserido = null;
                int indiceConta;
                double valorSaque;

                Console.Write("Digite o número da conta (ou X para Sair): ");
                ValorInserido = Console.ReadLine().ToUpper();

                if (ValorInserido != "X")
                    indiceConta = int.Parse(ValorInserido);
                else
                    return;

                Console.Write("Digite o valor a ser sacado (ou X para Sair): ");
                ValorInserido = Console.ReadLine().ToUpper().Replace(",",".");
                if (ValorInserido != "X")
                {
                    if(double.Parse(ValorInserido) <= 0)
                        valorSaque = double.Parse(ValorInserido + "a");
                    else
                        valorSaque = double.Parse(ValorInserido);
                }
                else
                    return;                   

                listContas[indiceConta].Sacar(valorSaque);
                return;
            }

            RetornaSemConta();
        }
        
        private static void ListarContas()
        {            
            if (listContas.Count == 0)
            {
                RetornaSemConta();
                return;
            }

            double totalSaldo = 0;
            double totalCredito = 0;
            double totalSaldoTotal = 0;
            int totalPF = 0;
            int totalPJ = 0;
            
            Console.WriteLine("\nConta\t\tNome\t\t    Tipo Conta             Saldo\t  Crédito      Saldo Total\n");
            for (int i = 0; i < listContas.Count; i++)
            {
                Conta conta = listContas[i];
                Console.Write("  {0}    ", i);
                Console.WriteLine(conta);
                totalCredito += listContas[i].somaCredito();
                totalSaldo += listContas[i].somaSaldo();
                totalSaldoTotal += listContas[i].somaSaldoTotal();
                if (listContas[i].retornaTipoConta() == "PessoaFisica")
                    totalPF++;
                else
                    totalPJ++;

            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------");
            Console.WriteLine("  TOTALIZADORES >>>>>{0, 44:0.00}{1, 16:0.00}{2, 16:0.00}", totalSaldo, totalCredito, totalSaldoTotal);
            Console.WriteLine("\n    < TOTAL CONTAS >\n\n   Pessoa Física{0, 5}\n   Pessoa Jurídica{1, 3}\n     TOTAL >>>>{2, 6}", totalPF, totalPJ, listContas.Count);
        }

        private static void InserirConta()
        {
            string opcao = null;
            while (opcao != "1" || opcao != "2" || opcao != "X")
            {
                Console.WriteLine("Opções de preenchimento de nova conta:\n  1 - Para preenchimento automático (atualiza com 5 contas previamente cadastradas a fim de viabilizar os testes)\n  2 - Para preenchimento manual\n  X - Para Sair\n");
                opcao = Console.ReadLine().ToUpper();
                switch (opcao)
                {
                    case "1":

                        PreencheContas();
                        return;
                    
                    case "2":

                        string opc_tp_cta = null;                        
                        while (opc_tp_cta != "1" || opc_tp_cta != "2" || opc_tp_cta != "X")
                        {  
                            Console.WriteLine("Informe o tipo de conta\n  ( 1 ) Pessoa Fisica\n  ( 2 ) Pessoa Juridica\n  ( X ) Sair");
                            opc_tp_cta = Console.ReadLine().ToUpper(); 
                            if (opc_tp_cta == "1" || opc_tp_cta == "2")
                                break;
                            else if (opc_tp_cta == "X")
                                return;
                        }
                        int entradaTipoConta = int.Parse(opc_tp_cta);
                        
                        Console.Write("Digite o Nome do Cliente: ");
                        string entradaNome = Console.ReadLine().ToUpper();
                        
                        double entradaSaldo;
                        try
                        {
                            entradaSaldo = Convert.ToDouble(TrataDouble("Digite o saldo (ou X para Sair): "));    
                        }
                        catch
                        {
                            return;
                        }

                        double entradaCredito;
                        try
                        {
                            entradaCredito = Convert.ToDouble(TrataDouble("Digite o crédito (ou X para Sair): "));    
                        }
                        catch
                        {
                            return;
                        }

                        Conta novaConta = new Conta(tipoConta: (TipoConta)entradaTipoConta,
                                                    saldo: entradaSaldo,
                                                    credito: entradaCredito,
                                                    nome: entradaNome);
                        listContas.Add(novaConta);
                        return;
                    
                    case "X":
                        return;
                }
            }
        }

        private static string TrataDouble(string mensagem)
        {
            string pre_valor = "";
            string retornar = null;
            while (TrataErroDouble(pre_valor) == 0 || pre_valor != "X")
            {
                Console.Write(mensagem);
                pre_valor = Console.ReadLine().ToUpper();
                if (TrataErroDouble(pre_valor) > 0)
                {
                    retornar = Convert.ToString(TrataErroDouble(pre_valor));
                    break;
                }
                else if (pre_valor == "X")
                {
                    retornar = pre_valor;
                    break;
                }
            }
            return retornar;
        }

        private static double TrataErroDouble(string val_inf)
        {
            try
            {
                Convert.ToDouble(val_inf);
                string pre_val;
                try 
                {
                    pre_val = val_inf.Replace(",",".");
                    Convert.ToDouble(pre_val);
                }
                catch
                {
                    pre_val = val_inf;
                }
                return Convert.ToDouble(pre_val);
            }
            catch
            {
                return 0;
            }
        }

        private static void PreencheContas()
        {
            int[] tipos_conta = new int [] {1, 1, 2, 1, 2};
            string[] nomes = new string [] {"MARIA AUGUSTA", "CLAUDIA PEREIRA", "TRANSPORTES DO NORTE", "CLÁUDIO ROBERTO", "S.A INCORPORAÇÕES"};
            double[] saldos = new double [] {400.00, 800.00, 15000.00, 700.00, 50000.00};
            double[] creditos = new double[] {400.00, 400.00, 1500.00, 400.00, 12000.00};

            for ( int i = 0; i <= 4; i++ )
            {
                Conta novaConta = new Conta(tipoConta: (TipoConta)tipos_conta[i],
                                            saldo: saldos[i],
                                            credito: creditos[i],
                                            nome: nomes[i]);

                listContas.Add(novaConta);
            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine("\nDIO Bank a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:\n");
            Console.WriteLine("1 - Listar contas");
            Console.WriteLine("2 - Inserir nova conta");
            Console.WriteLine("3 - Transferir");
            Console.WriteLine("4 - Sacar");
            Console.WriteLine("5 - Depositar");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair\n");
            string opcaoUsuario = Console.ReadLine().ToUpper();
            return opcaoUsuario;
        }

        private static void RetornaSemConta()
        {
            Console.WriteLine("\nNão existem contas cadastradas. Para realizar operações cadastre uma conta\npara o presente cliente. Após, retorne para transacionar o desejado.");
        }
    }
}
