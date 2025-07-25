 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="BillDesk.aspx.cs" Inherits="BillDesk" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Bill Desk</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Bill Desk</li> 
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
                                      
                                       <!-- <select class="form-control">
                                            <option>All</option>
                                            <option>Another Center</option>
                                            <option>Other Center</option>
                                        </select>-->
                                         <asp:TextBox id="txtCenter"  runat="server" Visible="false"  placeholder="Enter Center" class="form-control pull-right"></asp:TextBox>
                                    <asp:DropDownList ID="ddlcenter" Visible="true"  runat="server" autopostback="false" 
                                class="form-control">
                             </asp:DropDownList>
                                         </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label></label>
                                      
                                         <asp:textbox id="txtname" runat="server"  placeholder="Enter Patient Name" class="form-control pull-right" >     </asp:textbox>
                                    </div>
                                </div>
                                </div>
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       <asp:textbox id="txtmobileno"  placeholder="Enter Mobile Number" runat="server" class="form-control pull-right" >   </asp:textbox>
                                    </div>
                                </div>
                                
                                <div class="col-sm-3">
                                    <div class="form-group">
                                                                               
                                      <asp:textbox id="txtregno" runat="server"  placeholder="EnterReg No"  class="form-control pull-right">  </asp:textbox>
                                    </div>
                                </div> 
                                <div class="col-sm-2">
                                    <div class="form-group">
                                       
                                       <asp:button id="btnshow" OnClientClick="return validate();" runat="server" class="btn btn-primary" text="Click" onclick="btnshow_Click" 
                    tabindex="6" />
                                        
                                    </div>
                                </div> 
                                <div class="col-sm-2">
                                    <asp:Label runat="server" ForeColor="Green" Font-Bold="true" Font-Size="Larger" ID="LblTcharge" ></asp:Label>        
                    
                                    
                                </div>    
                                 <div class="col-sm-2">
                                            
                     <asp:Label runat="server" ID="LblPAmt"></asp:Label>    
                                    
                                </div>                   
                            </div>
                       
                     
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
                                 <table width="100%">
                   <tr>
                       <td >
                           
<asp:Label runat="server" ID="Label1" Font-Bold="true" ForeColor="Red" Width="70px" Text=""  ></asp:Label>
                       </td>
                       <td>
                           <asp:Label runat="server" ID="Label3" Font-Bold="true" ForeColor="Red" Width="70px" Text=""  ></asp:Label>
                       </td>
                       <td align="right">
<asp:Label runat="server" ID="a" Font-Bold="true"  ForeColor="Red" Text="Charges: "  ></asp:Label>
                       </td>
                       <td align="center">
                           <asp:Label runat="server" ID="lblcharges" Font-Size="Larger" ForeColor="Blue" Font-Bold="true" ></asp:Label>
                       </td>
                  
                       <td align="right">
<asp:Label runat="server" ID="Label2" Font-Bold="true" ForeColor="Red" Text="Paid"></asp:Label>
                       </td>
                       <td align="center">
                           <asp:Label runat="server" Font-Bold="true"  Font-Size="Larger" ForeColor="Blue" ID="lbldramt" ></asp:Label>
                       </td>
                   <td>
                       <asp:Label runat="server" ID="Label4" Font-Bold="true" ForeColor="Red" Text="Discount"></asp:Label>
                   </td>
                       <td>
                            <asp:Label runat="server" Font-Bold="true"  Font-Size="Larger" ForeColor="Blue" ID="lbldisamt" ></asp:Label>
                       </td>
                       <td>
                       <asp:Label runat="server" ID="Label5" Font-Bold="true" ForeColor="Red" Text="Balance"></asp:Label>
                   </td>
                       <td>
                            <asp:Label runat="server" Font-Bold="true"  Font-Size="Larger" ForeColor="Blue" ID="lblbal" ></asp:Label>
                       </td>
                    
                   </tr>

               </table>
                <asp:gridview id="GV_Billdesk" datakeynames="PID" class="table table-responsive table-sm table-bordered" runat="server" width="100%" autogeneratecolumns="False"
                    allowsorting="True" onrowediting="GV_Billdesk_RowEditing" allowpaging="True" onpageindexchanging="GV_Billdesk_PageIndexChanging"
                    pagesize="15000"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   onrowdeleting="GV_Billdesk_RowDeleting" OnRowDataBound="GV_Billdesk_RowDataBound">
                   
<AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                   
                    <columns>
                       <asp:CommandField ButtonType="Button" FooterStyle-ForeColor="Black" HeaderText="Pay Bill"  EditText="Pay" ShowEditButton="True">

                           <FooterStyle ForeColor="Black">

                                 </FooterStyle>
                                <ItemStyle Width="50px" />
                                <ControlStyle Width="50px" />
                            </asp:CommandField>
                             <asp:CommandField ButtonType="Button" Visible="true" FooterStyle-ForeColor="Black"  HeaderText="Bill Receipt"  DeleteText="Bill" ShowDeleteButton="True">
                                 <FooterStyle ForeColor="Black">

                                 </FooterStyle>
                                <ItemStyle Width="50px" />
                                <ControlStyle Width="50px" />
                            </asp:CommandField>
                             <asp:TemplateField HeaderText="Receipt" Visible="true">
                            <ItemTemplate>                                
                               
                            <asp:DropDownList ID="ddlReceipt" AutoPostBack="true" Visible="true"  runat="server" 
                                    onselectedindexchanged="ddlReceipt_SelectedIndexChanged"></asp:DropDownList>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Balance" HeaderText="Status"></asp:BoundField>
                     <asp:BoundField DataField="Patregdate" HeaderText="Date " />
                            <asp:BoundField DataField="PatRegID" HeaderText="R-No" />
                            <asp:BoundField DataField="Fullname" HeaderText="Name">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Age" HeaderText="Age" />
                              <asp:BoundField DataField="sex" HeaderText="Gender" />
                             <asp:BoundField DataField="Drname" HeaderText="Refer Dr">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="Centername" HeaderText="Center " />
                            <asp:TemplateField HeaderText="Test ">
                                <ItemTemplate>
                                    <asp:Label ID="lbltestname" Text='<%#Bind("Testname")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TestCharges" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblTestCharges" Text='<%#Bind("TestCharges")%>' runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:BoundField DataField="TestCharges" HeaderText=" Charges"></asp:BoundField>
                              
                            <asp:BoundField DataField="AmtPaid" HeaderText="Paid"></asp:BoundField>
                           
                            <asp:BoundField DataField="Discount" HeaderText="Discount"></asp:BoundField>
                             <asp:BoundField DataField="Balance" HeaderText="Balance"></asp:BoundField>
                              
                          
                         
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <asp:HiddenField ID="hdnPID" runat="server" Value='<%#Eval("PID") %>' />
                                    <asp:HiddenField ID="hdn_BillNo" runat="server" Value='<%#Eval("BillNo") %>' />
                                    <asp:HiddenField ID="hdntest" runat="server" Value='<%#Bind("Tests")%>' />
                                </ItemTemplate>
                                <ItemStyle Width="0px" />
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnCentercode" runat="server" Value='<%#Bind("Centercode")%>' />
                                      <asp:HiddenField ID="Hdnfid" runat="server" Value='<%#Bind("FID")%>' />
                                      <asp:HiddenField ID="ISBTH" runat="server" Value='<%#Bind("IsbillBH")%>' />
                                </ItemTemplate>
                                <ItemStyle Width="0px" />
                            </asp:TemplateField>
                        </columns>
                        

<HeaderStyle ForeColor="Black"></HeaderStyle>
                        

                </asp:gridview>
                                
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
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
