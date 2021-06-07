using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MVCCRUDweb.Models;
using MVCCRUDweb.Models.ViewModels;

namespace MVCCRUDweb.Controllers
{
    public class Tabla1Controller : Controller
    {
        //Metodo para mostar a tabela, todos os registros
        // GET: Tabla1
        public ActionResult Index()
        {

            List<ListTablaViewModel> Lista;
            using (CURSOCSHARPEntities1 db = new CURSOCSHARPEntities1()) //Nome do Banco de dados, do EntityFramework
            {
                Lista = (from d in db.tabla1 //Recebe a tabela do banco, vai acessar toda tabela
                             select new ListTablaViewModel
                              {
                                 Id = d.id,
                                 Nombre = d.nombre,
                                 Correo = d.correo
                             }).ToList();
            }
           return View(Lista);
        }


        //Metodos para Adicionar no banco
        //CRIANDO UM NOVO METODO
        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CURSOCSHARPEntities1 db = new CURSOCSHARPEntities1()) //Acesso ao banco de dados
                    {
                        var oTabla = new tabla1();
                        oTabla.nombre = model.Nombre;
                        oTabla.correo = model.Correo;
                        oTabla.fecha_nascimento = model.Fecha_Nacimiento;

                        db.tabla1.Add(oTabla);
                        db.SaveChanges(); //Para salvar as modificacoes no banco de dados
                    }
                    return Redirect("~/Tabla1/"); // Para retornar a vista tabla. 
                }
                return View(model); //Para retornar a vista tabla

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        //Metodos para Editar um registro existente  //Editar para mostrar no banco
        public ActionResult Editar(int Id)//vai receber um ID, vai acessar o banco e mandar o registro procurado
        {
            TablaViewModel model = new TablaViewModel(); //Criando um objeto para receber os elementos da classe 
           using(CURSOCSHARPEntities1 db = new CURSOCSHARPEntities1()) //Acessando atraves do banco 
            {
                var oTabla = db.tabla1.Find(Id); //Busca o registro com o ID declarado
                model.Nombre = oTabla.nombre;
                model.Correo = oTabla.correo;
                model.Fecha_Nacimiento = (DateTime)oTabla.fecha_nascimento;
                model.Id = oTabla.id;
            }
                
           return View(model);
        }

        [HttpPost]
        public ActionResult Editar(TablaViewModel model) //Editar para inserir no banco os dados modificados
        {
            try
            {  if (ModelState.IsValid)//VAlida o estado da camada modelo. No caso a TablaviewModel 
                {
                    using (CURSOCSHARPEntities1 db = new CURSOCSHARPEntities1())
                    {
                        var oTabla = db.tabla1.Find(model.Id); //para acessar o banco com o ID 
                        oTabla.nombre = model.Nombre;
                        oTabla.correo = model.Correo;
                        oTabla.fecha_nascimento = model.Fecha_Nacimiento;

                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges(); //Salvando as modificacoes
                    }
                    return Redirect("~/Tabla1/"); //Redireciona para a TAbla1 
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
      
        [HttpGet]
        public ActionResult Eliminar(int Id)
        {
           using (CURSOCSHARPEntities1 bd = new CURSOCSHARPEntities1())
            {
                var oTabla = bd.tabla1.Find(Id);
                bd.tabla1.Remove(oTabla);
                bd.SaveChanges();
            }
            return Redirect("~/Tabla1/");
        }

    }
}