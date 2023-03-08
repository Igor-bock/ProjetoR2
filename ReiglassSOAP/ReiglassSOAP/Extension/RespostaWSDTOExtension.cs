using ReiglassSOAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReiglassSOAP.Extension
{
    public static class RespostaWSDTOExtension
    {

        public static void CMX_DefinirException(
            this RespostaWSDTO p_InstanciaADefinirAException, 
            Exception p_Excessao)
        {
            p_InstanciaADefinirAException.ErroCodigo = -1;
            p_InstanciaADefinirAException.ErroMensagem = p_Excessao.Message;
        }
    }
}