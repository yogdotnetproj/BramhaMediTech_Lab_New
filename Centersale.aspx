<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="Centersale.aspx.cs" Inherits="Centersale" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>

    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Center Sale</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Center Sale</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        
                                       <asp:dropdownlist id="ddlCenter" runat="server" class="form-control form-select" tabindex="3">
                </asp:dropdownlist>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                     <!--   <input type="text" class="form-control pull-right" placeholder="Enter Mobile Number"> -->
                                         <asp:textbox id="txtreferdoctor" runat="server" class="form-control pull-right" placeholder="Enter Ref Doc" tabindex="5">
                            </asp:textbox>
                             <cc1:AutoCompleteExtender id="AutoCompleteExtender3" runat="server" MinimumPrefixLength="1" 
                            TargetControlID="txtreferdoctor" ServiceMethod="GetDoctor"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="Div4">
                            </cc1:AutoCompleteExtender>
                                    </div>
                                </div> 
                                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   
                                      <asp:Button ID="btndoctorincome"  OnClientClick=" return validate();"   runat="server"
                class="btn btn-secondary" Text="Dr Income Report" OnClick="btndoctorincome_Click" >
            </asp:Button>
            <asp:Button ID="centerdetail"  OnClientClick=" return validate();"  OnClick="centerdetail_Click" runat="server"
                class="btn btn-info" Text="Center Wise Income Detail" >
            </asp:Button>
            <asp:Button ID="btnexcel"  OnClientClick=" return validate();"   runat="server"
                class="btn btn-primary" Text="Center Wise Income Excel" onclick="btnexcel_Click" >
            </asp:Button>
                    
                                                 
                                </div> 
                            </div>
                        </div>
                    </div>
                   
                </section>
                 <script language="javascript" type="text/javascript">
                     function OpenReport() {
                         window.open("Reports.aspx");
                     } 
               </script>
</asp:Content>

