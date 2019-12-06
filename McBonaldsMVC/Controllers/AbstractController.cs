using McBonaldsMVC.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace McBonaldsMVC.Controllers
{
    public class AbstractController : Controller
    {
        protected const string SESSION_EMAIL_CLIENTE = "cliente_email"; 
        protected const string SESSION_NOME_CLIENTE="cliente_nome"; 
        protected const string SESSION_TIPO_CLIENTE="cliente_Tipo";

        protected string ObterUsuarioSession()
        {
          var Usuario =  HttpContext.Session.GetString(SESSION_EMAIL_CLIENTE);
          if (!string.IsNullOrEmpty(Usuario))
          {
              return Usuario;
          }else{
              return "";
          }
        
        }
          protected string ObterUsuarioNomeSession()
        {
          var nomeUsuario =  HttpContext.Session.GetString(SESSION_NOME_CLIENTE);
          if (!string.IsNullOrEmpty(nomeUsuario))
          {
              return nomeUsuario;
          }else{
              return "";
          }
        
        }
        protected string ObterUsuarioTipoSession()
        {
          var TipoUsuario =  HttpContext.Session.GetString(SESSION_TIPO_CLIENTE);
          if (!string.IsNullOrEmpty(TipoUsuario))
          {
              return TipoUsuario;
          }else{
              return "";
          }
        
        }
    }
}