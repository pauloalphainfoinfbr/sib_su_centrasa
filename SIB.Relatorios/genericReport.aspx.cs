using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Configuration;
using SIB.Data;
using System.Data;

namespace SIB.Relatorios
{
    public partial class genericReport : System.Web.UI.Page
    {
        UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            String reportFileName = String.Empty, reportPath = String.Empty;
            DataTable dtSource = null;

            switch (HttpContext.Current.Session["rel"].ToString())
            {
                #region Vistoriados e Carregados
                case "vistoriadosEcarregados":
                    reportFileName = "rptVeiculosVistoriados.rpt";
                    reportPath = MapPath("rptVeiculosVistoriados.rpt");
                    dtSource = new ReportDataSources().dtsVistoriadosCarregados();

                    HttpContext.Current.Session["rel"] = "vistoriadosEcarregados";

                    HttpContext.Current.Session["dtIni"] = null;
                    HttpContext.Current.Session["dtFim"] = null;
                    HttpContext.Current.Session["tipoVeiculo"] = null;
                    HttpContext.Current.Session["tipoCarregamento"] = null;
                    HttpContext.Current.Session["tipoAssoalho"] = null;
                    HttpContext.Current.Session["ano"] = null;
                    HttpContext.Current.Session["tipoTransporte"] = null;
                    HttpContext.Current.Session["transportadora"] = null;
                    HttpContext.Current.Session["usinaDeposito"] = null;
                    HttpContext.Current.Session["dedicado"] = null;
                    HttpContext.Current.Session["tipoVinculo"] = null;
                    HttpContext.Current.Session["cargaDescarga"] = null;
                    HttpContext.Current.Session["placa"] = null;
                    HttpContext.Current.Session["tipoCarroceria"] = null;
                    HttpContext.Current.Session["catracaTodaAmarracao"] = null;
                    HttpContext.Current.Session["movimentouTampas"] = null;
                    HttpContext.Current.Session["bercoMetalicoBobineira"] = null;
                    HttpContext.Current.Session["dt"] = null;
                    break;
                #endregion

                #region Veículos Recusados
                case "veiculosRecusados":
                    reportFileName = "rptVeiculosRecusados.rpt";
                    reportPath = MapPath("rptVeiculosRecusados.rpt");
                    dtSource = new ReportDataSources().dtsRecusados();

                    HttpContext.Current.Session["rel"] = null;
                    HttpContext.Current.Session["dtIni"] = null;
                    HttpContext.Current.Session["dtFim"] = null;
                    HttpContext.Current.Session["placa"] = null;
                    HttpContext.Current.Session["motivo"] = null;
                    HttpContext.Current.Session["transportadora"] = null;
                    break;
                #endregion

                #region Veículos Recuperados
                case "veiculosRecuperados":
                    reportFileName = "rptVeiculosRecuperados.rpt";
                    reportPath = MapPath("rptVeiculosRecuperados.rpt");
                    dtSource = new ReportDataSources().dtsRecuperados();

                    HttpContext.Current.Session["rel"] = null;
                    HttpContext.Current.Session["dtIni"] = null;
                    HttpContext.Current.Session["dtFim"] = null;
                    HttpContext.Current.Session["transportadora"] = null;
                    HttpContext.Current.Session["motivo"] = null;
                    break;
                #endregion

                #region Relação de Checklists
                case "relacaoChecklists":
                    reportFileName = "rptChecklist.rpt";
                    reportPath = MapPath("rptChecklist.rpt");
                    dtSource = new ReportDataSources().dtsChecklists();

                    HttpContext.Current.Session["rel"] = null;
                    HttpContext.Current.Session["dtIni"] = null;
                    HttpContext.Current.Session["dtFim"] = null;
                    HttpContext.Current.Session["placa"] = null;
                    HttpContext.Current.Session["dt"] = null;
                    HttpContext.Current.Session["transportadora"] = null;
                    HttpContext.Current.Session["motivo"] = null;
                    HttpContext.Current.Session["catracaAmarracao"] = null;
                    HttpContext.Current.Session["movimentouTampas"] = null;
                    HttpContext.Current.Session["bercoMetalicoBobineira"] = null;
                    HttpContext.Current.Session["cargaSecaGraneleiro"] = null;
                    break;
                #endregion

                #region Material Danificado
                case "materialDanificado":
                    reportFileName = "rptMaterialDanificado.rpt";
                    reportPath = MapPath("rptMaterialDanificado.rpt");
                    dtSource = new ReportDataSources().dtsMaterialDanificado();

                    HttpContext.Current.Session["rel"] = null;
                    HttpContext.Current.Session["dtIni"] = null;
                    HttpContext.Current.Session["dtFim"] = null;
                    HttpContext.Current.Session["transportadora"] = null;
                    HttpContext.Current.Session["notaFiscal"] = null;
                    HttpContext.Current.Session["numLote"] = null;
                    HttpContext.Current.Session["placa"] = null;
                    break;
                #endregion

                #region Tempo de Permanência
                case "tempoPermanencia":
                    reportFileName = "relDuracaoVeiculo.rpt";
                    reportPath = MapPath("relDuracaoVeiculo.rpt");
                    dtSource = new ReportDataSources().dtsTempoPermanencia();

                    HttpContext.Current.Session["rel"] = null;
                    HttpContext.Current.Session["dtIni"] = null;
                    HttpContext.Current.Session["dtFim"] = null;
                    HttpContext.Current.Session["transportadora"] = null;
                    HttpContext.Current.Session["horaPeacao"] = null;
                    HttpContext.Current.Session["placa"] = null;
                    break;
                #endregion

                #region Tempo de Peação
                case "tempoPeacao":
                    reportFileName = "rptTempoPeacao.rpt";
                    reportPath = MapPath("rptTempoPeacao.rpt");
                    dtSource = new ReportDataSources().dtsTempoPeacao();

                    HttpContext.Current.Session["rel"] = null;
                    HttpContext.Current.Session["dtIni"] = null;
                    HttpContext.Current.Session["dtFim"] = null;
                    HttpContext.Current.Session["transportadora"] = null;
                    HttpContext.Current.Session["horaPeacao"] = null;
                    HttpContext.Current.Session["placa"] = null;
                    break;
                #endregion

                #region Fumaça Negra
                case "fumacaNegra":
                    reportFileName = "rptFumacaNegra.rpt";
                    reportPath = MapPath("rptFumacaNegra.rpt");
                    dtSource = new ReportDataSources().dtsFumacaNegra();

                    HttpContext.Current.Session["rel"] = null;
                    HttpContext.Current.Session["dtIni"] = null;
                    HttpContext.Current.Session["dtFim"] = null;
                    HttpContext.Current.Session["status"] = null;
                    HttpContext.Current.Session["transportadora"] = null;
                    HttpContext.Current.Session["placa"] = null;
                    break;
                #endregion

                #region DT
                case "dt":
                    reportFileName = "rptDT.rpt";
                    reportPath = MapPath("rptDT.rpt");
                    dtSource = new ReportDataSources().dtsDT();

                    HttpContext.Current.Session["rel"] = null;
                    HttpContext.Current.Session["dtIni"] = null;
                    HttpContext.Current.Session["dtFim"] = null;
                    HttpContext.Current.Session["transportadora"] = null;
                    HttpContext.Current.Session["tipoVeiculo"] = null;
                    HttpContext.Current.Session["tipoTransporte"] = null;
                    HttpContext.Current.Session["tipoAssoalho"] = null;
                    HttpContext.Current.Session["tipoCarroceria"] = null;
                    HttpContext.Current.Session["placa"] = null;
                    HttpContext.Current.Session["tipoCarregamento"] = null;
                    HttpContext.Current.Session["dt"] = null;
                    HttpContext.Current.Session["ano"] = null;
                    HttpContext.Current.Session["usinaDeposito"] = null;
                    break;
                #endregion

                #region Peso Escoado
                case "pesoEscoado":
                    reportFileName = "rptPesoTotalEscoado.rpt";
                    reportPath = MapPath("rptPesoTotalEscoado.rpt");
                    dtSource = new ReportDataSources().dtsPesoEscoado();

                    HttpContext.Current.Session["rel"] = null;
                    HttpContext.Current.Session["dtIni"] = null;
                    HttpContext.Current.Session["dtFim"] = null;
                    HttpContext.Current.Session["tipoVeiculo"] = null;
                    HttpContext.Current.Session["tipoCarregamento"] = null;
                    HttpContext.Current.Session["tipoAssoalho"] = null;
                    HttpContext.Current.Session["ano"] = null;
                    HttpContext.Current.Session["tipoTransporte"] = null;
                    HttpContext.Current.Session["transportadora"] = null;
                    HttpContext.Current.Session["usinaDeposito"] = null;
                    HttpContext.Current.Session["veiculoDedicado"] = null;
                    HttpContext.Current.Session["frotaAgregPart"] = null;
                    HttpContext.Current.Session["cargaDescarga"] = null;
                    HttpContext.Current.Session["placa"] = null;
                    HttpContext.Current.Session["tipoCarroceria"] = null;
                    HttpContext.Current.Session["catracaTodaAmarracao"] = null;
                    HttpContext.Current.Session["movimentouTampas"] = null;
                    HttpContext.Current.Session["bercoMetalicoBobineira"] = null;
                    HttpContext.Current.Session["cargaSecaGraneleiro"] = null;
                    HttpContext.Current.Session["dt"] = null;
                    break;
                #endregion

                #region Bloqueados
                case "bloqueados":
                    reportFileName = "rptVeiculosBloqueados.rpt";
                    reportPath = MapPath("rptVeiculosBloqueados.rpt");
                    dtSource = new ReportDataSources().dtsBloquados();

                    HttpContext.Current.Session["rel"] = null;
                    HttpContext.Current.Session["dtIni"] = null;
                    HttpContext.Current.Session["dtFim"] = null;
                    HttpContext.Current.Session["tipoVeiculo"] = null;
                    HttpContext.Current.Session["transportadora"] = null;
                    HttpContext.Current.Session["tipoVinculo"] = null;
                    HttpContext.Current.Session["cargaDescarga"] = null;
                    HttpContext.Current.Session["placa"] = null;
                    HttpContext.Current.Session["tipoCarroceria"] = null;
                    break;
                #endregion
            }

            System.Net.Mime.ContentDisposition cd = null;
            byte[] doc = null;
            string mimeType = "";
            ExportFormatType FormatType = ExportFormatType.PortableDocFormat;

            if (Convert.ToInt32(HttpContext.Current.Session["formato"]) == 0)
            {
                cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = "Report.pdf",
                    Inline = true,
                };

                mimeType = "application/pdf";
                FormatType = ExportFormatType.PortableDocFormat;
            }
            else if (Convert.ToInt32(HttpContext.Current.Session["formato"]) == 1)
            {
                cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = "Report.xls",
                    Inline = false,
                };

                mimeType = "application/excel";
                FormatType = ExportFormatType.ExcelRecord;
            }
            else if (Convert.ToInt32(HttpContext.Current.Session["formato"]) == 2)
            {
                cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = "Report.rtf",
                    Inline = false,
                };

                mimeType = "application/rtf";
                FormatType = ExportFormatType.EditableRTF;
            }
            else if (Convert.ToInt32(HttpContext.Current.Session["formato"]) == 3)
            {
                cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = "Report.xls",
                    Inline = false,
                };

                mimeType = "application/excel";
                FormatType = ExportFormatType.Excel;
            }
            else if (Convert.ToInt32(HttpContext.Current.Session["formato"]) == 4)
            {
                cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = "Report.xlsx",
                    Inline = false,
                };

                mimeType = "application/excel";
                FormatType = ExportFormatType.ExcelWorkbook;
            }

            try
            {

                using (ReportDocument crReportDocument = new ReportDocument())
                {
                    crReportDocument.FileName = reportFileName;
                    crReportDocument.Load(reportPath);
                    crReportDocument.SetDataSource(dtSource);

                    using (System.IO.Stream oStream = crReportDocument.ExportToStream(FormatType))
                    {
                        doc = new byte[oStream.Length];
                        oStream.Read(doc, 0, Convert.ToInt32(oStream.Length - 1));
                        oStream.Close();
                        oStream.Dispose();
                    }

                    crReportDocument.Close();
                }

                Response.ClearContent();
                Response.ClearHeaders();

                Response.AddHeader("Content-Length", doc.Length.ToString());
                Response.AppendHeader("Content-Disposition", cd.ToString());
                Response.AddHeader("Expires", "0");
                Response.AddHeader("Pragma", "cache");
                Response.AddHeader("Cache-Control", "private");

                Response.ContentType = mimeType;
                Response.AddHeader("Accept-Ranges", "bytes");

                Response.BinaryWrite(doc);
                Response.Flush();

                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            catch (Exception ex)
            {
                HttpContext.Current.ApplicationInstance.Response.Write("Não foi possível imprimir o relatório. Msg: " + ex.Message);
            }
            finally
            {
                _dbContext.Dispose();
            }
        }

        [System.Web.Services.WebMethod]
        public static void vistoriadosEcarregados(string dtIni, string dtFim, string tipoVeiculo,
            string tipoCarregamento, string tipoAssoalho, string ano, string tipoTransporte,
            string transportadora, string usinaDeposito, string dedicado, string tipoVinculo,
            string cargaDescarga, string placa, string tipoCarroceria, string catracaTodaAmarracao,
            string movimentouTampas, string bercoMetalicoBobineira, string dt, string formato)
        {
            HttpContext.Current.Session["rel"] = "vistoriadosEcarregados";

            HttpContext.Current.Session["dtIni"] = dtIni;
            HttpContext.Current.Session["dtFim"] = dtFim;
            HttpContext.Current.Session["tipoVeiculo"] = tipoVeiculo;
            HttpContext.Current.Session["tipoCarregamento"] = tipoCarregamento;
            HttpContext.Current.Session["tipoAssoalho"] = tipoAssoalho;
            HttpContext.Current.Session["ano"] = ano;
            HttpContext.Current.Session["tipoTransporte"] = tipoTransporte;
            HttpContext.Current.Session["transportadora"] = transportadora;
            HttpContext.Current.Session["usinaDeposito"] = usinaDeposito;
            HttpContext.Current.Session["dedicado"] = dedicado;
            HttpContext.Current.Session["tipoVinculo"] = tipoVinculo;
            HttpContext.Current.Session["cargaDescarga"] = cargaDescarga;
            HttpContext.Current.Session["placa"] = placa;
            HttpContext.Current.Session["tipoCarroceria"] = tipoCarroceria;
            HttpContext.Current.Session["catracaTodaAmarracao"] = catracaTodaAmarracao;
            HttpContext.Current.Session["movimentouTampas"] = movimentouTampas;
            HttpContext.Current.Session["bercoMetalicoBobineira"] = bercoMetalicoBobineira;
            HttpContext.Current.Session["dt"] = dt;

            HttpContext.Current.Session["formato"] = formato;
        }

        [System.Web.Services.WebMethod]
        public static void veiculosRecusados(string dtIni, string dtFim, string placa,
            string motivo, string transportadora, string formato)
        {
            HttpContext.Current.Session["rel"] = "veiculosRecusados";

            HttpContext.Current.Session["dtIni"] = dtIni;
            HttpContext.Current.Session["dtFim"] = dtFim;
            HttpContext.Current.Session["placa"] = placa;
            HttpContext.Current.Session["motivo"] = motivo;
            HttpContext.Current.Session["transportadora"] = transportadora;

            HttpContext.Current.Session["formato"] = formato;
        }

        [System.Web.Services.WebMethod]
        public static void veiculosRecuperados(string dtIni, string dtFim, string motivo,
            string transportadora, string formato)
        {
            HttpContext.Current.Session["rel"] = "veiculosRecuperados";

            HttpContext.Current.Session["dtIni"] = dtIni;
            HttpContext.Current.Session["dtFim"] = dtFim;
            HttpContext.Current.Session["transportadora"] = transportadora;
            HttpContext.Current.Session["motivo"] = motivo;

            HttpContext.Current.Session["formato"] = formato;
        }

        [System.Web.Services.WebMethod]
        public static void relacaoChecklists(string dtIni, string dtFim, string placa, string dt, string transportadora,
            string motivo, string catracaAmarracao, string movimentouTampas, string bercoMetalicoBobineira, 
            string cargaSecaGraneleiro, string formato)
        {
            HttpContext.Current.Session["rel"] = "relacaoChecklists";

            HttpContext.Current.Session["dtIni"] = dtIni;
            HttpContext.Current.Session["dtFim"] = dtFim;
            HttpContext.Current.Session["placa"] = placa;
            HttpContext.Current.Session["dt"] = dt;
            HttpContext.Current.Session["transportadora"] = transportadora;
            HttpContext.Current.Session["motivo"] = motivo;
            HttpContext.Current.Session["catracaAmarracao"] = catracaAmarracao;
            HttpContext.Current.Session["movimentouTampas"] = movimentouTampas;
            HttpContext.Current.Session["bercoMetalicoBobineira"] = bercoMetalicoBobineira;
            HttpContext.Current.Session["cargaSecaGraneleiro"] = cargaSecaGraneleiro;

            HttpContext.Current.Session["formato"] = formato;
        }

        [System.Web.Services.WebMethod]
        public static void materialDanificado(string dtIni, string dtFim, string transportadora,
            string numLote, string placa, string formato)
        {
            HttpContext.Current.Session["rel"] = "materialDanificado";

            HttpContext.Current.Session["dtIni"] = dtIni;
            HttpContext.Current.Session["dtFim"] = dtFim;
            HttpContext.Current.Session["transportadora"] = transportadora;
            HttpContext.Current.Session["numLote"] = numLote;
            HttpContext.Current.Session["placa"] = placa;

            HttpContext.Current.Session["formato"] = formato;
        }

        [System.Web.Services.WebMethod]
        public static void tempoPermanencia(string dtIni, string dtFim, string transportadora,
            string horaPeacao, string placa, string formato)
        {
            HttpContext.Current.Session["rel"] = "tempoPermanencia";

            HttpContext.Current.Session["dtIni"] = dtIni;
            HttpContext.Current.Session["dtFim"] = dtFim;
            HttpContext.Current.Session["transportadora"] = transportadora;
            HttpContext.Current.Session["horaPeacao"] = horaPeacao;
            HttpContext.Current.Session["placa"] = placa;

            HttpContext.Current.Session["formato"] = formato;
        }

        [System.Web.Services.WebMethod]
        public static void tempoPeacao(string dtIni, string dtFim, string transportadora,
            string horaPeacao, string placa, string formato)
        {
            HttpContext.Current.Session["rel"] = "tempoPeacao";

            HttpContext.Current.Session["dtIni"] = dtIni;
            HttpContext.Current.Session["dtFim"] = dtFim;
            HttpContext.Current.Session["transportadora"] = transportadora;
            HttpContext.Current.Session["horaPeacao"] = horaPeacao;
            HttpContext.Current.Session["placa"] = placa;

            HttpContext.Current.Session["formato"] = formato;
        }

        [System.Web.Services.WebMethod]
        public static void fumacaNegra(string dtIni, string dtFim, string status, string transportadora, string placa, string formato)
        {
            HttpContext.Current.Session["rel"] = "fumacaNegra";

            HttpContext.Current.Session["dtIni"] = dtIni;
            HttpContext.Current.Session["dtFim"] = dtFim;
            HttpContext.Current.Session["status"] = status;
            HttpContext.Current.Session["transportadora"] = transportadora;
            HttpContext.Current.Session["placa"] = placa;

            HttpContext.Current.Session["formato"] = formato;
        }

        [System.Web.Services.WebMethod]
        public static void dt(string dtIni, string dtFim, string transportadora, 
            string tipoVeiculo, string tipoTransporte, string tipoAssoalho, string tipoCarroceria,
            string placa, string tipoCarregamento, string dt, string ano, string usinaDeposito, string formato)
        {
            HttpContext.Current.Session["rel"] = "dt";

            HttpContext.Current.Session["dtIni"] = dtIni;
            HttpContext.Current.Session["dtFim"] = dtFim;
            HttpContext.Current.Session["transportadora"] = transportadora;
            HttpContext.Current.Session["tipoVeiculo"] = tipoVeiculo;
            HttpContext.Current.Session["tipoTransporte"] = tipoTransporte;
            HttpContext.Current.Session["tipoAssoalho"] = tipoAssoalho;
            HttpContext.Current.Session["tipoCarroceria"] = tipoCarroceria;
            HttpContext.Current.Session["placa"] = placa;
            HttpContext.Current.Session["tipoCarregamento"] = tipoCarregamento;
            HttpContext.Current.Session["dt"] = dt;
            HttpContext.Current.Session["ano"] = ano;
            HttpContext.Current.Session["usinaDeposito"] = usinaDeposito;

            HttpContext.Current.Session["formato"] = formato;
        }

        [System.Web.Services.WebMethod]
        public static void pesoEscoado(string dtIni, string dtFim, string tipoVeiculo,
            string tipoCarregamento, string tipoAssoalho, string ano, string tipoTransporte,
            string transportadora, string usinaDeposito, string veiculoDedicado, string frotaAgregPart, 
            string cargaDescarga, string placa, string tipoCarroceria, string catracaTodaAmarracao, 
            string movimentouTampas, string bercoMetalicoBobineira, string cargaSecaGraneleiro, 
            string dt, string formato)
        {
            HttpContext.Current.Session["rel"] = "pesoEscoado";

            HttpContext.Current.Session["dtIni"] = dtIni;
            HttpContext.Current.Session["dtFim"] = dtFim;
            HttpContext.Current.Session["tipoVeiculo"] = tipoVeiculo;
            HttpContext.Current.Session["tipoCarregamento"] = tipoCarregamento;
            HttpContext.Current.Session["tipoAssoalho"] = tipoAssoalho;
            HttpContext.Current.Session["ano"] = ano;
            HttpContext.Current.Session["tipoTransporte"] = tipoTransporte;
            HttpContext.Current.Session["transportadora"] = transportadora;
            HttpContext.Current.Session["usinaDeposito"] = usinaDeposito;
            HttpContext.Current.Session["veiculoDedicado"] = veiculoDedicado;
            HttpContext.Current.Session["frotaAgregPart"] = frotaAgregPart;
            HttpContext.Current.Session["cargaDescarga"] = cargaDescarga;
            HttpContext.Current.Session["placa"] = placa;
            HttpContext.Current.Session["tipoCarroceria"] = tipoCarroceria;
            HttpContext.Current.Session["catracaTodaAmarracao"] = catracaTodaAmarracao;
            HttpContext.Current.Session["movimentouTampas"] = movimentouTampas;
            HttpContext.Current.Session["bercoMetalicoBobineira"] = bercoMetalicoBobineira;
            HttpContext.Current.Session["cargaSecaGraneleiro"] = cargaSecaGraneleiro;
            HttpContext.Current.Session["dt"] = dt;

            HttpContext.Current.Session["formato"] = formato;
        }

        [System.Web.Services.WebMethod]
        public static void bloqueados(string dtIni, string dtFim, string tipoVeiculo,
            string transportadora, string tipoVinculo, string cargaDescarga, string placa,
            string tipoCarroceria, string formato)
        {
            HttpContext.Current.Session["rel"] = "bloqueados";

            HttpContext.Current.Session["dtIni"] = dtIni;
            HttpContext.Current.Session["dtFim"] = dtFim;
            HttpContext.Current.Session["tipoVeiculo"] = tipoVeiculo;
            HttpContext.Current.Session["transportadora"] = transportadora;
            HttpContext.Current.Session["tipoVinculo"] = tipoVinculo;
            HttpContext.Current.Session["cargaDescarga"] = cargaDescarga;
            HttpContext.Current.Session["placa"] = placa;
            HttpContext.Current.Session["tipoCarroceria"] = tipoCarroceria;

            HttpContext.Current.Session["formato"] = formato;
        }

        //protected void Page_Unload(object sender, EventArgs e)
        //{
        //    crReportDocument.Close();
        //    crReportDocument.Dispose();
        //    _dbContext.Dispose();
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    //GC.Collect();
        //    //GC.SuppressFinalize(this);
        //}

        //public override void Dispose()
        //{
        //    base.Dispose();

        //    crReportDocument.Close();
        //    crReportDocument.Dispose();
        //    _dbContext.Dispose();
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    //GC.Collect();
        //    //GC.SuppressFinalize(this);
        //}

        //protected override void OnUnload(EventArgs e)
        //{
        //    base.OnUnload(e);

        //    crReportDocument.Close();
        //    crReportDocument.Dispose();
        //    _dbContext.Dispose();
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();
        //    //GC.Collect();
        //    //GC.SuppressFinalize(this);
        //}
    }
}