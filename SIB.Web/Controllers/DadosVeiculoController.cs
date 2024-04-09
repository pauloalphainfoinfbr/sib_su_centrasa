using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using SIB.Data;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Web;
using System.Web.Mvc;

namespace SIB.Web.Controllers
{
    [Authorize]
    public class DadosVeiculoController : BaseController
    {        
        //Busca condutor para a pesquisa quando digitado o CPF
        [HttpPost]
        public JsonResult BuscaCondutor(string numCPF)
        {
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

            if (_dbContext.Database.Connection.State == ConnectionState.Closed)
                _dbContext.Database.Connection.Open();

            var condutor = _dbContext.condutor.Where(c => c.cpf_condutor == numCPF.Replace(".","").Replace("-","")).ToList();
            string nomeCondutor = (condutor != null && condutor.Count > 0) ? condutor[0].nome_condutor : "";

            return Json(nomeCondutor, JsonRequestBehavior.AllowGet);
        }

        //Dados Veiculo
        public ActionResult Index(Models.Empresa.DadosVeiculo dadosVeiculo)
        {
            if(Session["usuario"] != null)
            {
                if (Session["fromButton"] != null)
                {
                    Session["fromButton"] = null;

                    UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
                    int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
                    string sessionID = Session.SessionID;
                    Multiton multiCheck = Multiton.GetInstance(sessionID);

                    //Popula Lista "Transportadoras"
                    Models.Auxiliares.ListaEmpresa le = new Models.Auxiliares.ListaEmpresa();
                    ViewBag.ListaEmpresa = le.getEmpresa(2);
                    ViewBag.idEmpresaSelecionada = multiCheck.check.id_emp_transportadora;                                       

                    dadosVeiculo.placa = multiCheck.check.placa;
                    dadosVeiculo.dataCheckList = multiCheck.check.data_checklist != null ? (DateTime)multiCheck.check.data_checklist : DateTime.Now;
                    dadosVeiculo.transporte = multiCheck.check.id_tipo_transporte != null ? Convert.ToInt32(multiCheck.check.id_tipo_transporte) : 1;
                    dadosVeiculo.vinculo = multiCheck.check.id_tipo_frete != null ? Convert.ToInt32(multiCheck.check.id_tipo_frete) : 2;
                    dadosVeiculo.dedicado = multiCheck.check.chk_veicDedicado != null ? Convert.ToInt32(multiCheck.check.chk_veicDedicado) : 2;
                    dadosVeiculo.cargaDescarga = multiCheck.check.chk_cargaDescarga != null ? Convert.ToInt32(multiCheck.check.chk_cargaDescarga) : 1;
                    dadosVeiculo.usinaDeposito = multiCheck.check.chk_usinaDeposito != null ? Convert.ToInt32(multiCheck.check.chk_usinaDeposito) : 1;                        
                    dadosVeiculo.alturaVeic = multiCheck.check.chk_alturaVeiculo != null ? Convert.ToInt32(multiCheck.check.chk_alturaVeiculo) : 1;

                    //Verifica se já foi realizado Laudo ha menos de 6 meses, se sim,
                    //carrega o campo "Possui laudo" e o campo "Data de Realização do Laudo"

                    if (_dbContext.checklist.Where(c => c.placa == multiCheck.check.placa.Replace("-", "")).ToList().Count > 0)
                    {
                        if (!string.IsNullOrWhiteSpace(_dbContext.checklist.Where(c => c.placa == multiCheck.check.placa.Replace("-", "")).OrderByDescending(c => c.data_checklist).FirstOrDefault().chk_possuiLaudoOpacidade.ToString()))
                        {
                            if (Convert.ToInt32(_dbContext.checklist.Where(c => c.placa == multiCheck.check.placa.Replace("-", "")).OrderByDescending(c => c.data_checklist).FirstOrDefault().chk_possuiLaudoOpacidade) == 1)
                            {
                                TimeSpan tempoPassado = (Convert.ToDateTime(DateTime.Now.ToShortDateString()) - Convert.ToDateTime(_dbContext.checklist.Where(c => c.placa == multiCheck.check.placa.Replace("-", "")).OrderByDescending(c => c.data_checklist).FirstOrDefault().data_realizacao_laudoOpacidade.ToString()));

                                //Se ainda não completou um ano que o laudo foi realizado, carrega a data 
                                //do ultimo laudo, se não, o usuário terá que informar os campos "Possui laudo" e
                                //"Data Realizacao do laudo".

                                if (tempoPassado.Days < 365)
                                {
                                    dadosVeiculo.possuiLaudoOpacidade = 1;
                                    dadosVeiculo.dataRealizacaoLaudoOpacidade = _dbContext.checklist.Where(c => c.placa == multiCheck.check.placa.Replace("-", "")).OrderByDescending(c => c.data_checklist).FirstOrDefault().data_realizacao_laudoOpacidade.ToString();
                                }

                                //Quando tiver faltando 30 dias para vencimento do laudo, o sistema deve ficar
                                //emitindo a mensagem até o usuário inserir a data de um novo laudo
                                if (tempoPassado.Days > 335 && tempoPassado.Days < 365)
                                {
                                    ViewBag.QtdeDiasParaLaudoExpirar = (365 - tempoPassado.Days);
                                }
                                else if (tempoPassado.Days > 364)
                                {
                                    ViewBag.LaudoExpirado = tempoPassado.Days;
                                }
                            }
                        }
                    }
                    else
                    {
                        dadosVeiculo.possuiLaudoOpacidade = 1;
                    }

                    string placa = multiCheck.check.placa.Replace("-", "");                    

                    if (multiCheck.check.veiculo.ano == null)
                        dadosVeiculo.ano = _dbContext.veiculo.Where(r => r.placa == multiCheck.check.placa).ToList().Count > 0 ? _dbContext.veiculo.Where(r => r.placa == multiCheck.check.placa).ElementAt(0).ano : -1;
                    else
                        dadosVeiculo.ano = multiCheck.check.veiculo.ano;

                    if (multiCheck.check.cpf_condutor == null){

                        if (_dbContext.checklist.Where(r => r.placa == placa).ToList().Count > 0){
                            string cpf = _dbContext.checklist.Where(r => r.placa == placa).OrderByDescending(r => r.id_checklist).ToList()[0].cpf_condutor;

                            dadosVeiculo.cpfCondutor = cpf;
                            dadosVeiculo.nomeCondutor = _dbContext.condutor.Where(r => r.cpf_condutor == cpf).ToList()[0].nome_condutor;
                        }
                        else{
                            dadosVeiculo.cpfCondutor = "";
                            dadosVeiculo.nomeCondutor = "";
                        }
                    }
                    else {
                        //Se já existi condutor                

                        if (_dbContext.condutor.Where(r => r.cpf_condutor == multiCheck.check.cpf_condutor).ToList().Count == 0){
                            //Se já possui condutor cadastrado no banco, carrega a tela com os dados do Banco
                            dadosVeiculo.cpfCondutor = multiCheck.check.condutor.cpf_condutor;
                            dadosVeiculo.nomeCondutor = multiCheck.check.condutor.nome_condutor;
                        }
                        else{
                            //Se não possui condutor cadastrado, carrega a tela com os dados armazenados na classe ContextBaseController
                            var condutor = _dbContext.condutor.Where(r => r.cpf_condutor == multiCheck.check.cpf_condutor).ToList();
                            dadosVeiculo.cpfCondutor = condutor[0].cpf_condutor;
                            dadosVeiculo.nomeCondutor = condutor[0].nome_condutor;
                        }
                    }                    

                    return View(dadosVeiculo);                    
                }
                else
                {
                    return RedirectToAction("Login", "Account");                        
                }
            }            
        
            return View(dadosVeiculo);
        }

        [HttpPost, ActionName("Index")]
        [Models.Empresa.Submit(Models.Empresa.SubmitRequirement.StartsWith, "Proximo")]
        public ActionResult Proximo(Models.Empresa.DadosVeiculo dadosVeiculo)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton Checklist = Multiton.GetInstance(sessionID);

            if (dadosVeiculo.ano == -1)
                ModelState.AddModelError("", "Por favor, informe o ano do veículo.");

            Models.Validadores.CustomValidationCPF cpf = new Models.Validadores.CustomValidationCPF();

            if (string.IsNullOrWhiteSpace(dadosVeiculo.cpfCondutor))
            {
                ModelState.AddModelError("", "CPF Obrigatório.");
            }
            else
            {
                if (!cpf.ValidarCPF(dadosVeiculo.cpfCondutor))
                    ModelState.AddModelError("", "CPF inválido.");
            }

            if(string.IsNullOrWhiteSpace(dadosVeiculo.nomeCondutor))
                ModelState.AddModelError("", "Informe o nome do condutor.");

            if(dadosVeiculo.transportadora == 0)
                ModelState.AddModelError("", "Informe a transportadora.");

            if (dadosVeiculo.transporte == 0 || dadosVeiculo.vinculo == 0 || dadosVeiculo.dedicado == 0 || dadosVeiculo.cargaDescarga == 0 || dadosVeiculo.usinaDeposito == 0)
                ModelState.AddModelError("", "Os campos \"Frota/Agregado/Particular\", \"Frete\", \"Veiculo Dedicado\", \"Carga/Descarga/Troca de Nota\", \"USINA/DEPÓSITO\", \"Arqueamento da carroceria de acordo com a legislação\" e \"Possui Laudo de Opacidade de 100%\" são obrigatórios.");

            DateTime dateToTest;

            if (dadosVeiculo.possuiLaudoOpacidade == 0)
                ModelState.AddModelError("", "O campo \"Possui Laudo de Opacidade de 100%\" é obrigatório.");
            else if (dadosVeiculo.possuiLaudoOpacidade == 1 && string.IsNullOrWhiteSpace(dadosVeiculo.dataRealizacaoLaudoOpacidade))
                    ModelState.AddModelError("", "Deve ser informada a data de realização do Laudo de Opacidade 100%.");
            else if (dadosVeiculo.possuiLaudoOpacidade == 1 && !DateTime.TryParse(dadosVeiculo.dataRealizacaoLaudoOpacidade, out dateToTest))
                ModelState.AddModelError("", "Insira uma Data válida para a Data de Realização do Laudo.");
            else if (dadosVeiculo.possuiLaudoOpacidade == 1 && Convert.ToDateTime(dadosVeiculo.dataRealizacaoLaudoOpacidade) > DateTime.Now)
                ModelState.AddModelError("", "Data de Realização do Laudo não pode ser maior que a data de hoje.");            
            else if (dadosVeiculo.possuiLaudoOpacidade == 1 && (((TimeSpan)(DateTime.Now - Convert.ToDateTime(dadosVeiculo.dataRealizacaoLaudoOpacidade))).Days > 365))
                ModelState.AddModelError("", "Data de Realização do Laudo não pode ter mais que 365 dias (1 Ano).");            


                if (ModelState.IsValid)
                {
                    Session["fromButton"] = 1;

                    //Verifica se já existe o Condutor cadastrado              
                    if (_dbContext.condutor.Where(r => r.cpf_condutor == dadosVeiculo.cpfCondutor).ToList().Count == 0)
                    {
                        Checklist.check.condutor = new Data.condutor();
                        Checklist.check.condutor.cpf_condutor = dadosVeiculo.cpfCondutor.Replace(".", "").Replace("-", "");
                        Checklist.check.condutor.nome_condutor = dadosVeiculo.nomeCondutor;
                        Checklist.check.cpf_condutor = dadosVeiculo.cpfCondutor.Replace(".", "").Replace("-", "");
                    }
                    else
                    {
                        var condutor = _dbContext.condutor.Where(r => r.cpf_condutor == dadosVeiculo.cpfCondutor).ToList()[0];
                        Checklist.check.condutor = condutor;
                        Checklist.check.condutor.nome_condutor = dadosVeiculo.nomeCondutor;
                        Checklist.check.cpf_condutor = dadosVeiculo.cpfCondutor.Replace(".", "").Replace("-", "");
                    }

                    //Veiculo
                    if (Checklist.check.veiculo == null)
                    {
                        Checklist.check.veiculo = new Data.veiculo();
                    }

                    Checklist.check.veiculo.placa = dadosVeiculo.placa.Replace("-", "");
                    Checklist.check.veiculo.ano = dadosVeiculo.ano;
                    Checklist.check.placa = dadosVeiculo.placa.Replace("-", "");
                    Checklist.check.id_tipo_transporte = dadosVeiculo.transporte;
                    Checklist.check.id_tipo_frete = dadosVeiculo.vinculo;
                    Checklist.check.id_emp_transportadora = Convert.ToInt32(dadosVeiculo.transportadora);
                    Checklist.check.chk_veicDedicado = dadosVeiculo.dedicado;
                    Checklist.check.chk_cargaDescarga = dadosVeiculo.cargaDescarga;
                    Checklist.check.chk_usinaDeposito = dadosVeiculo.usinaDeposito;
                    Checklist.check.chk_alturaVeiculo = dadosVeiculo.alturaVeic;
                    Checklist.check.chk_possuiLaudoOpacidade = dadosVeiculo.possuiLaudoOpacidade;

                    if (dadosVeiculo.possuiLaudoOpacidade == 1)
                        Checklist.check.data_realizacao_laudoOpacidade = Convert.ToDateTime(dadosVeiculo.dataRealizacaoLaudoOpacidade);
                    else
                        Checklist.check.data_realizacao_laudoOpacidade = null;

                    return RedirectToAction("Index", "DadosVeiculo2");
                }
                else
                {
                    Multiton multiCheck = Multiton.GetInstance(sessionID);

                    //Popula Lista "Transportadoras"
                    Models.Auxiliares.ListaEmpresa le = new Models.Auxiliares.ListaEmpresa();
                    ViewBag.ListaEmpresa = le.getEmpresa(2);
                    ViewBag.idEmpresaSelecionada = dadosVeiculo.transportadora;

                    return View("Index", dadosVeiculo);
                }
        }

        [ActionName("Index")]
        [Models.Empresa.Submit("Voltar")]
        public ActionResult Voltar(Models.Empresa.EstadoConservacao dadosEstadoConservacao)
        {
            Session["fromButton"] = 1;
            return RedirectToAction("Index", "BuscaVeiculo");
        }
    }
}
