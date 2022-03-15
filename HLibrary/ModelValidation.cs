using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HLibrary
{
    public class ModelValidation
    {
        public List<ValidationResult> Result(object model)
        {
            var resultadoValidacao = new List<ValidationResult>();
            var contexto = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, contexto, resultadoValidacao, true);

            return resultadoValidacao;
        }
    }
    public class VerificaCPF : ValidationAttribute
    {
        public VerificaCPF()
        {
            ErrorMessage = "CPF inválido";
        }

        public override bool IsValid(object value)
        {
            var cpfcnpj = value as string;

            if (CpfCnpj.ValidaCpf(CpfCnpj.LimpaCpfCnpj(cpfcnpj)))
                return true;
            return false;
        }
    }

    public class ValidaCEP : ValidationAttribute
    {
        public ValidaCEP()
        {
            ErrorMessage = "CEP inválido";
        }

        public override bool IsValid(object value)
        {
            string cp = value as string;

            string cep = Funcoes.RemoverCaracterEspecial(cp);

            if (string.IsNullOrEmpty(cep))
                return false;

            if (cep.Length < 8)
                return false;

            return true;
        }
    }
    public class ValidaTel : ValidationAttribute
    {
        public ValidaTel()
        {
            ErrorMessage = "Telefone inválido";
        }

        public override bool IsValid(object value)
        {
            string tl = value as string;

            string tel = Funcoes.RemoverCaracterEspecial(tl);

            if (string.IsNullOrEmpty(tel))
                return false;

            if (tel.Length <= 10)
                return false;

            return true;
        }
    }
}
