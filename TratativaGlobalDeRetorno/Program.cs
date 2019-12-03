using System;
using System.Collections.Generic;
using System.Linq;

namespace TratativaGlobalDeRetorno
{
    public class Program
    {
        static void Main(string[] args)
        {
            var mensagem = new Mensagem();

            mensagem.AdicionarInformacoes(new Informacao("Informações 01"));
            Console.WriteLine(mensagem.InformacoesConcatenado);

            mensagem.AdicionarAlertas(new Alerta("Alerta 01"), new Alerta("Alerta 02"));
            mensagem.AdicionarAlertas(new Alerta("Alerta 03"));
            Console.WriteLine(mensagem.AlertasConcatenado);

            mensagem.AdicionarErros(new Erro("Erro 01"), new Erro("Erro 02"));
            Console.WriteLine(mensagem.ErrosConcatenado);

            Console.ReadKey();
        }
    }

    public class Mensagem : IMensagem
    {
        public List<Informacao> Informacoes { get; } = new List<Informacao>();
        public List<Alerta> Alertas { get; } = new List<Alerta>();
        public List<Erro> Erros { get; } = new List<Erro>();

        public string InformacoesConcatenado => ObterInformacoes();
        public string AlertasConcatenado => ObterAlertas();
        public string ErrosConcatenado => ObterErros();

        public void AdicionarInformacoes(params Informacao[] informacoes)
        {
            if (informacoes != null)
                Informacoes.AddRange(informacoes);

            return;
        }

        public IMensagem AdicionarAlertas(params Alerta[] alertas)
        {
            if (alertas != null)
                Alertas.AddRange(alertas);

            return this;
        }

        public IMensagem AdicionarErros(params Erro[] erros)
        {
            if (erros != null)
                Erros.AddRange(erros);

            return this;
        }

        public string ObterInformacoes()
        {
            if (Informacoes.Any())
                return string.Join(", ", Informacoes.Select(item => $"{ item.Mensagem }"));

            return string.Empty;
        }

        public string ObterAlertas()
        {
            if(Alertas.Any())
                return string.Join(", ", Alertas.Select(item => $"{ item.Mensagem }"));

            return string.Empty;
        }

        public string ObterErros()
        {
            if (Erros.Any())
                return Erros.Select(item => string.Format("{0}", item.Mensagem)).Aggregate((texto, next) => texto + ", " + next);

            return string.Empty;
        }        
    }

    public interface IMensagem
    {
        List<Informacao> Informacoes { get; }
        List<Alerta> Alertas { get; }
        List<Erro> Erros { get; }

        void AdicionarInformacoes(params Informacao[] informacoes);
        IMensagem AdicionarAlertas(params Alerta[] alertas);
        IMensagem AdicionarErros(params Erro[] erros);

        string ObterInformacoes();
        string ObterAlertas();
        string ObterErros();        
    }

    public abstract class MensagemBase
    {
        public string Mensagem { get; set; }
    }

    public class Informacao : MensagemBase
    {
        public Informacao(string informacao)
        {
            Mensagem = informacao;
        }

        public override string ToString() => $"Informação: { Mensagem }";
    }

    public class Alerta : MensagemBase
    {
        public Alerta(string alerta)
        {
            Mensagem = alerta;
        }

        public override string ToString() => $"Alerta: { Mensagem }";
    }

    public class Erro : MensagemBase
    {
        public Erro(string erro)
        {
            Mensagem = erro;
        }

        public override string ToString() => $"Erro: { Mensagem }";
    }
}