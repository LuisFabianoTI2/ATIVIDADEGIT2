using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        
     public IActionResult ListaDeUsuarios()
     {
         Autenticacao.CheckLogin(this);
         Autenticacao.verificaSeUsuarioEAdmin(this);
         return View(new UsuarioService().Listar());
     }






      public IActionResult RegistrarUsuario()
     {
         Autenticacao.CheckLogin(this);
         Autenticacao.verificaSeUsuarioEAdmin(this);
         return View();
     }



        [HttpPost]



        public IActionResult RegistrarUsuario(Usuario novoUsuario)
     {
         novoUsuario.Senha = Criptografo.TextoCriptografado(novoUsuario.Senha);
         new  UsuarioService().incluirUsuario(novoUsuario);
         return RedirectToAction("CadastroRealizado");
     }
      


    public IActionResult CadastroRealizado()
    {
        return View();
    }
    


    public IActionResult editarUsuario(int id)
    {
        Usuario u = new UsuarioService().Listar(id);
        return View(u);
    }


    [HttpPost]


    public IActionResult editarUsuario(Usuario  userEditado)
    {
        new UsuarioService().editarUsuario(userEditado);
        return RedirectToAction("ListaDeUsuarios");
    }




    public IActionResult excluirUsuario (int id)
    {
        return View (new UsuarioService().Listar(id));
    }


    [HttpPost]


    public IActionResult excluirUsuario(string decisao, int id)
    {

        if (decisao == "EXCLUIR")
        {
            ViewData["Mensagem"] = "Exclusão do usuario" + new UsuarioService().Listar(id).Nome + "realiza exclusao";
            new UsuarioService().excluirUsuario(id);
            return View ("ListaDeUsuarios", new UsuarioService().Listar());
        }
        else
        {
            ViewData["Mensagem"] = "Exclusão Cancelada";
            return View("ListadeUsuarios",  new UsuarioService().Listar());
        }
    }
     
    
    public IActionResult Sair()
     {
         HttpContext.Session.Clear();
         return RedirectToAction("Index", "Home");
     }



        public IActionResult NeedAdmin()
        {

           Autenticacao.CheckLogin(this);
           return View();

        }


    }
}