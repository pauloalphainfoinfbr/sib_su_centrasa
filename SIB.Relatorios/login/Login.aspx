<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SIB.Relatorios.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
   <title>CEB - CHECK-LIST ELETRÔNICO BRASILTEC</title>
    
    <meta http-equiv="Content-type" content="text/html; charset=utf-8" />
	<script type="text/javascript" src="../scripts/jquery-1.9.1.min.js"></script>		
	<script type="text/javascript" src="../Content/bootstrap-3.3.4-dist/js/bootstrap.min.js"></script>
	<link rel="stylesheet" href="../Content/css/main.css" />
	<link rel="stylesheet" href="../Content/bootstrap-3.3.4-dist/css/bootstrap.min.css" />		
	<link rel="stylesheet" href="../Content/font-awesome-4.3.0/css/font-awesome.min.css" />		 
	<link href="../Content/css/estilo.css" rel="stylesheet" />
	<link href="../Content/css/painel.css" rel="stylesheet" />	
		
	<style type="text/css">
		html,
		body {
			height: 95%;
			/* The html and body elements cannot have any padding or margin when creating a footer*/ 
		}
			
		#footer {
			height: 85px;
			background-color: #f5f5f5;
		}
	</style>
</head>
    <body>	
		<!-- Wrap all page content here -->
    	<div id="wrap">
			<!-- Modal -->
			<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
			  <div class="modal-dialog">
			    <div class="modal-content">
			      <div class="modal-header">
			        <!-- <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>-->
			        <h4 class="modal-title" id="myModalLabel"><i class="fa fa-info-circle"></i> Atenção!</h4>
			      </div>
			      <div class="modal-body">		        		       
					<p>Usuário ou senha inválidos.</p>					        			        			
			      </div>
			      <div class="modal-footer">
			        <button type="button" class="btn btn-primary" data-dismiss="modal">Ok</button>		        
			      </div>
			    </div>
			  </div>
			</div>
			
			<nav class="navbar navbar-default navbar-fixed-top">
			  <div class="container-fluid">
				<!-- Brand and toggle get grouped for better mobile display -->
				<div class="navbar-header">
				  <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
					<span class="sr-only">Toggle navigation</span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
					<span class="icon-bar"></span>
				  </button>
				  <!--<a class="navbar-brand" href="#">Usialpha</a>-->
				  
				  <a class="navbar-brand" href="#" style="padding-top:3px; padding-bottom:3px;">
					<%--<img alt="Brand" src="public/imgs/logomarca.png" class="img-responsive logo" />--%>
                    CEB - SU BETIM
				  </a>
				</div>
	
				<!-- Collect the nav links, forms, and other content for toggling -->
				<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
				  <ul class="nav navbar-nav navbar-right">
				  </ul>
				</div><!-- /.navbar-collapse -->
			  </div><!-- /.container-fluid -->
			</nav>
			
			<div class="container-fluid" style="margin-top:40px;">
				<div class="row-fluid">
					<div class="col-xs-12">
						
						<div class="panel panel-primary form-login">
							<div class="panel-heading" style="background-color:#46A55D;">
								<h3 class="panel-title panel-primary">Efetue o Login</h3>
						    </div>
                            <div style="margin-left:15px; margin-right:15px;">
								<img alt="Logo" src="../Content/imgs/nova-logo.jpg" class="img-responsive" />
						    </div>
						  <div class="panel-body">
							<form id="frmLogin" runat="server">
								<label class="sr-only" for="inputEmail">Usuário:</label>
									<div class="input-group margin-bottom-sm">
										<span class="input-group-addon" style="padding-left:20px;">Login:</span>
                                        <asp:TextBox runat="server" ID="nomeUsuario" CssClass="form-control input-md" MaxLength="50"></asp:TextBox>
										<%--<input type="text" id="txtUsuario" name="post[txtUsuario]" class="form-control input-md" placeholder="Usuário" maxlength="50" required />--%>
									</div>
								<label class="sr-only" for="inputPass">Senha:</label>
									<div class="input-group margin-bottom-sm">
										<span class="input-group-addon" style="padding-left:14px;">Senha:</span>
                                        <asp:TextBox TextMode="Password" runat="server" ID="senha" CssClass="form-control input-md" MaxLength="15"></asp:TextBox>
										<%--<input type="password" id="txtSenha" name="post[txtSenha]" class="form-control input-md" placeholder="Senha" maxlength="15" required />--%>
									</div>
                                <%--<asp:Button ID="entrar" runat="server" Text="Entrar" onclick="entrar_Click" CssClass="btn btn-default btn-sm btn-block" style="background-color:#46A55D; color:white;" /> --%>
								<button id="Button1" runat="server" onserverclick="entrar_Click"  class="btn btn-default btn-sm btn-block" style="background-color:#46A55D; color:white;">
									Acessar
								</button>																													
							</form>
						  </div>
						  <div class="panel-footer">
                              <label id="msg" runat="server" style="color:red; font-size:11px;" />
							<!-- <div class="checkbox">
								<label>
									<input type="checkbox" value="s" /> Lembre-me
								</label>
							</div> -->	
						  </div>
						</div>
						
					</div>
				</div>
	      </div>
      </div><!-- Wrap Div end -->
      <div id="footer">
          <div class="container">
	          <div class="row">
		          <div class="col-lg-12 text-center">
			          <p class="muted credit" style="font-size:12px; color:#00974B;">
				          Desenvolvido por Alphainfo Tecnologia Ltda - Tel. (31) 3351-3543 / 2564-0801 -  <a href="http://www.alphainfo.inf.br" target="_blank">www.alphainfo.inf.br</a>
			          </p>
		          </div>
	          </div>
          </div>
        </div>
	</body>
</html>

