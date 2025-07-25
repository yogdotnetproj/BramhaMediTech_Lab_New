<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="CenterSaleSummary.aspx.cs" Inherits="CenterSaleSummary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

                <!-- Content Header (Page header) -->
     <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Center sale summary</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Center sale summary</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        <div class="input-group date"  data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="fromdate" runat="server"  class="form-control">
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                               
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        <div class="input-group date"  data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="todate" runat="server" class="form-control">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                       <div class="col-sm-3">
                                           <asp:Button ID="btncensale" OnClientClick=" return validate();"  OnClick="btncensale_Click"  runat="server" CssClass="btn btn-primary" Text="Center sale Summary">            </asp:Button>
                            
                                       </div>
                               
                                               
                            </div>
                        </div>
                        
                    </div>
                   
                </section>
                <!-- /.content -->
           
              <script language="javascript" type="text/javascript">
                  function OpenReport() {
                      window.open("Reports.aspx");
                  } 
               </script>
</asp:Content>

