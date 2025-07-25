 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master"  CodeFile="Outsourcepatient.aspx.cs" Inherits="Outsourcepatient" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
            
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Outsource Patient</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Outsource Patient</li> 
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
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  CssClass="form-control pull-right"  tabindex="1">
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
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  CssClass="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                      
                 <asp:textbox id="txtPatientName" runat="server" placeholder="Enter Patient Name" CssClass="form-control" tabindex="3">
                </asp:textbox>
                                    </div>
                                </div>
                               
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                      
                 <asp:textbox id="txtmobileno" runat="server" placeholder="Enter Mobile No" CssClass="form-control" tabindex="4">
                </asp:textbox>
                                    </div>
                                </div>
                                </div>
                            <div class="row mb-3">
                               <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                      
                <asp:TextBox ID="txtregno"  runat="server" placeholder="Enter Reg No"  class="form-control" TabIndex="5">
                </asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                      
                <asp:TextBox id="txtCenter" tabIndex="6" runat="server" placeholder="Enter Center" CssClass="form-control" AutoPostBack="True" 
                                            ontextchanged="txtCenter_TextChanged" ></asp:TextBox>
                <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 185px; HEIGHT: 100px" id="Div2"></DIV>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" CompletionListElementID="Div2" ServiceMethod="Getcenter" TargetControlID="txtCenter" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                     <asp:TextBox id="txtOutsourceLab" tabIndex="6" runat="server" placeholder="Enter Outsource Lab" CssClass="form-control" AutoPostBack="false" 
                                            ></asp:TextBox>
                                    </div>
                                    </div>
                                <div class="col-sm-3">
                                               <asp:button id="btnshow" OnClientClick="return validate();" runat="server" text="Click" onclick="btnshow_Click" CssClass="btn btn-primary" tabindex="7" />
                               
                                </div>
                                               
                            </div>
                      
                       
                    </div>
                    <div class="box">
                        <div class="box-body">
                             <div class="table-responsive" style="width:100%">
                <asp:GridView ID="GV_Outcourcepat" class="table table-responsive table-sm table-bordered" runat="server" AutoGenerateColumns="False"
                    Width="100%" DataKeyNames="PID,Patmstid,PatRegID"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"  
 OnPageIndexChanging="GV_Outcourcepat_PageIndexChanging"
                    PageSize="50">
<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="PatRegID" HeaderText="RegNo" SortExpression="PatRegID" />
                        <asp:BoundField DataField="Name" HeaderText=" Name" ReadOnly="True" SortExpression="Name" />
                        <asp:BoundField DataField="sex" HeaderText="Gender" SortExpression="sex" />
                        <asp:BoundField DataField="age" HeaderText="Age" ReadOnly="True" SortExpression="age" />
                        <asp:BoundField DataField="mdy" />
                        <asp:BoundField DataField="CenterName" HeaderText="Center " SortExpression="CenterName">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DrName" HeaderText="Ref Doc " SortExpression="DrName">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>

                        <asp:BoundField DataField="TestNames" HeaderText="Test " SortExpression="TestNames">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                       
                       <asp:BoundField DataField="BarcodeID" HeaderText="BarCode " SortExpression="BarcodeID">
                            <ItemStyle Width="50px" />
                        </asp:BoundField>
                      
                         <asp:BoundField DataField="OutsourceLabName" HeaderText="Outsource LabName " SortExpression="OutsourceLabName">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                       

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblTestCodes" runat="server" Text='<%#Eval("MTCode") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblTestCharges" runat="server" Text='<%#Eval("TestCharges") %>' Visible="false"></asp:Label>
                                <asp:HiddenField ID="hdnFFID" runat="server" Value='<%#Eval("FID") %>' />
                                <asp:HiddenField ID="hdnstatus" runat="server" Value='<%#Eval("IspheboAccept") %>' />
                                 <asp:HiddenField ID="hdnpid" runat="server" Value='<%#Eval("PID") %>' />
                                   <asp:Label ID="hdnsampletype" runat="server" Text='<%#Eval("SampleType") %>' Visible="false"></asp:Label>
                                <asp:HiddenField ID="label20" runat="server" />
                                <asp:Label ID="lblTest" runat="server" Text='<%#Eval("Tests") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                   

<HeaderStyle ForeColor="Black"></HeaderStyle>
                   

                </asp:GridView>
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
                </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
           
            <!-- /.content-wrapper -->
            
         <script language="javascript" type="text/javascript">
             function OpenReport() {
                 window.open("Reports.aspx");
             } 
               </script>
               
    </asp:Content>