 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="Paybilldesk.aspx.cs" Inherits="Paybilldesk" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

        <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    

            <!-- Content Wrapper. Contains page content -->
          
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Pay Bill Desk</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Pay Bill Desk</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">                   
                            <div class="row mb-3">
                                <div class="col-sm-6">
                                     <div class="box">
                        <div class="box-body">
                                                 <div class="row mb-3">
                            <div class="col-sm-4"><span><strong>Reg # : <asp:Label ID="lblRegNo" runat="server" Text="RegNo"  Font-Bold="True" Width="70px" style="color:#07adc5;text-align:left"></asp:Label></strong></span></div>
                         
                                <div class="col-sm-8"><span><strong>Name :  <asp:Label ID="lblName" runat="server" Text="Name"  Font-Bold="True" Width="186px" style="color:#07adc5;text-align:left"></asp:Label></strong></span></div>
                                              </div>
                                    <div class="row mb-3">
                                <div class="col-sm-4"><span><strong>Age :  <asp:Label ID="lblage" runat="server" Text="Age"  Font-Bold="True" Width="51px" style="color:#07adc5;text-align:left"></asp:Label></strong></span></div>
                              
                                <div class="col-sm-8"><span><strong>Gender :  <asp:Label ID="lblSex" runat="server" Text="Sex"  Font-Bold="True" Width="70px" style="color:#07adc5;text-align:left"></asp:Label></strong></span></div>
                                               </div>
                                      <div class="row mb-3">
                                <div class="col-sm-4"><span><strong>Mobile : <asp:Label ID="LblMobileno" runat="server" Width="96px" 
                                Font-Bold="True" style="color:#07adc5;text-align:left"></asp:Label></strong></span></div>
                           
                                <div class="col-sm-8"><span><strong>Center :  <asp:Label ID="Lblcenter" runat="server" Width="175px" 
                                Font-Bold="True" style="color:#07adc5;text-align:left"></asp:Label></strong></span></div>
                                        </div>
                                    <div class="row mb-3">

                                <div class="col-sm-4"><span><strong>Reg Date : <asp:Label ID="Label1" runat="server"  Font-Bold="True" Width="142px" style="color:#07adc5;text-align:left"></asp:Label></strong></span></div>
                               
                                <div class="col-sm-8"><span><strong>Ref Dr. :<asp:Label ID="LblRefDoc" Font-Bold="True" runat="server"  Width="180px" style="color:#07adc5;text-align:left"></asp:Label></strong></span></div>
                               
                               </div>
                                      <div class="row mb-3">
                                 <div class="col-sm-4"><span><strong>Current Date : <asp:Label ID="txtBillDate" runat="server" Text=""  Font-Bold="True" Width="70px" style="color:#07adc5;text-align:left"></asp:Label></strong></span></div>
                              
                                <div class="col-sm-8"><span><strong>Bill No :  <asp:Label ID="lblBillNo" runat="server" Text="0"  Font-Bold="True" style="color:#07adc5;text-align:left"></asp:Label></strong></span></div>


            <asp:Label ID="Label3" runat="server" Text=" " Width="51px" Font-Bold="True"></asp:Label>
                              
                          
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <div class="table-responsive" style="width:100%">
                                    <asp:GridView ID="GridView2" class="table table-responsive table-sm table-bordered" runat="server" AutoGenerateColumns="False" Width="100%"
                                       
        HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   OnRowDataBound="GridView2_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="tdate" HeaderText="Bill Date" />
                                            <asp:BoundField DataField="AmtPaid" HeaderText="Amount Paid" />
                                            <asp:BoundField DataField="Paymenttype" HeaderText="Mode Of Payment" />
                                             <asp:BoundField DataField="tdate" HeaderText="Transcation Date" DataFormatString="{0:dd/MM/yyyy}"
                                                HtmlEncode="False" />
                                            <asp:BoundField DataField="tdate" HeaderText="Transcation Time" />
                                           
                                            <asp:BoundField DataField="username" HeaderText="Transcation User" />
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                </div>
                            </div>
                                    </div>
                                </div>
                                      <div id="Div1" class="col-lg-12" runat="server" visible="false">
                                    <div class="form-group">
                                       
                                       
                                          <<div id="div2" class="rounded_corners"  style="overflow: scroll; height: 175px; width: 655px">
                            <asp:GridView ID="GridView1" Visible="false" 
        HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"    runat="server" Width="655px" AutoGenerateColumns="False"
                                 OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="100">
                                <Columns>
                                    <asp:BoundField DataField="Tests" HeaderText="Test Code" />
                                    <asp:TemplateField HeaderText="Test Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTestName" runat="server" Text='<%#Eval("testname_M") %>'>&apos;&gt;</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Test Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTestRate" runat="server" Text='<%#Eval("testrate") %>'>&apos;&gt;</asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                                    </div>
                                </div>
                                         <div class="col-sm-4" style="display:none">
                                    <div class="form-group">
                                        <label>Discount Given By</label>
                                        <div class="row mb-3">
                                            <div class="col-lg-12 col-xs-12">
                                                <asp:Label ID="lblDiscountuser" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                              <div class="col-sm-2">
                                    <div class="form-group">
                                        <label>Net Amt.</label>
                                        <div class="row mb-3">
                                            <div class="col-lg-12 col-xs-12">
                                                
                                                <asp:TextBox id="txtNetPayment" runat="server"  CssClass="form-control" placeholder="Net Amount" ReadOnly="true" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                




                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Tax Amt.</label>
                                   
                                             
                                                <asp:TextBox id="txthstamount" style="width:100%" runat="server" Enabled="false" CssClass="form-control" placeholder="HST Amount" ></asp:TextBox>
                                           
                                    </div>
                                </div>
                                </div>

                                    </div>
                                <div class="col-sm-6">
                                    <div class="box" style="min-height:361px">
                        <div class="box-body">
                            <div class="row mb-3">
                                  
                          
                           
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Bill Amount</label>
                                        <div class="row mb-3">
                                            <div class="col-lg-12 col-xs-12">
                                                <span> <asp:Label ID="lbltestcharges" runat="server" Text="" Font-Bold="true" ForeColor="Blue" Width="102px">
                    </asp:Label></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Test Charges</label>
                                        <div class="row mb-3">
                                            <div class="col-lg-12 col-xs-12">
                                                <span> <asp:Label ID="LblTestCharg" runat="server" Text="" Font-Bold="true" ForeColor="Orange" Width="102px">
                    </asp:Label></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label><asp:Label ID="lblAdvance" runat="server" Text="Advance Amount Paid:">
                        </asp:Label></label>
                                        <div class="row mb-3">
                                            <div class="col-lg-12 col-xs-12">
                                                <span><asp:Label ID="lblAdvanceAmt" ForeColor="Green" Font-Bold="true" runat="server"></asp:Label></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                </div>
                             <div class="row mb-3">
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Other Charges</label>
                                        <div class="row mb-3">
                                            <div class="col-lg-12 col-xs-12">
                                                <span> <asp:TextBox ID="txtOtherCharges" runat="server" Text="" class="form-control" AutoPostBack="True" OnTextChanged="txtOtherCharges_TextChanged" ></asp:TextBox></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-sm-8">
                                    <div class="form-group">
                                        <label><asp:Label ID="Label4" runat="server" Text="Other Charge Remark:">
                        </asp:Label></label>
                                        <div class="row mb-3">
                                            <div class="col-lg-12 col-xs-12">
                                               <span> <asp:TextBox ID="txtOtherchargeRemark" runat="server" Text="" CssClass="form-control" >
                    </asp:TextBox></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                 </div>
                            
                            
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Discount Type *</label>
                                 
                                                                         
<asp:RadioButtonList id="RadioButtonList2" runat="server" Width="156px" Enabled="False" 
AutoPostBack="True" RepeatDirection="Horizontal" CssClass="form-check"
                            OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
<%--<asp:ListItem >Percent</asp:ListItem>--%>
<asp:ListItem Selected="True">Amt</asp:ListItem>
</asp:RadioButtonList> 


                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label><asp:Label id="lbldis" runat="server">Discount </asp:Label> </label>
                                        <div class="row mb-3">
                                            <div class="col-lg-12 col-xs-12">
                                                
                                                <asp:TextBox id="txtDiscnt" runat="server" Enabled="false" CssClass="form-control" 
                                                    OnTextChanged="txtDiscnt_TextChanged" AutoPostBack="True" ></asp:TextBox> 

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                            <div class="col-sm-4">
                                    <div class="form-group">
                                        Discoun Given by:
                                       
                                          <asp:RadioButtonList ID="RblDiscgiven"  Enabled="false"  runat="server" 
                RepeatDirection="Horizontal" AutoPostBack="True" CssClass="form-check"
                 TabIndex="12" onselectedindexchanged="RblDiscgiven_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="1" Text="Lab"></asp:ListItem>
              
                 <asp:ListItem Value="2" Text="Dr"></asp:ListItem>
                <asp:ListItem Value="3" Text="Both"></asp:ListItem>
                 
            </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                                       <asp:TextBox ID="txtDisLabgiven" Visible="false" CssClass="form-control" placeholder="Dis Lab"  Width="100px"  Height="30px" 
                                                         runat="server" AutoPostBack="True" ontextchanged="txtDisLabgiven_TextChanged"></asp:TextBox>
                                        <asp:TextBox ID="txtDisdrgiven" Visible="false" CssClass="form-control" placeholder="Dis Dr"  Width="100px"  Height="30px" 
                                                         runat="server" AutoPostBack="True" ontextchanged="txtDisdrgiven_TextChanged"></asp:TextBox>
                                        
                                </div>
                                 
                               
                                </div>
                            <div class="row mb-3">
                                             <div class="col-sm-4">
                                                      <label><asp:Label ID="Label2" runat="server" Text=" Payment Type"></asp:Label></label>
                                             </div>
                                                    <div class="col-sm-8">
                                                            <div class="form-group">
                                    
<asp:RadioButtonList id="RadioButtonList1" runat="server"  AutoPostBack="True" CssClass="form-check"
                                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" 
                                            RepeatDirection="Horizontal">
<asp:ListItem >Cash</asp:ListItem>  
    <asp:ListItem>Cheque</asp:ListItem>
<asp:ListItem >Card</asp:ListItem>
     <asp:ListItem>Online</asp:ListItem>
     
     
</asp:RadioButtonList>
                                    </div>
                                                    </div>

                                                         </div>
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label> <asp:Label ID="Label24" runat="server" Text="Amount Paid"></asp:Label></label>
                                        <div class="row mb-3">
                                            <div class="col-lg-12 col-xs-12">
                                                
                                                
<asp:TextBox id="txtAmtPaid" runat="server" CssClass="form-control" placeholder="Amount Paid" OnTextChanged="txtAmtPaid_TextChanged" AutoPostBack="True"></asp:TextBox> 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Balance</label>
                                        <div class="row mb-3">
                                            <div class="col-lg-12 col-xs-12">
                                               
                                                  <asp:TextBox id="txtBalance" runat="server" CssClass="form-control" placeholder="Balance" Enabled="False" ReadOnly="True"></asp:TextBox> 
                                             
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Label ID="lblamtbalance" runat="server" Visible="false"></asp:Label>
                                </div>
                              <!--  ===================================== -->
                               <div class="col-sm-4" Visible="false" style="display:none;">
                                    <div class="form-group">
                                        <label><asp:Label id="lblNo" runat="server" Text="Label" Visible="False"></asp:Label> </label>
                                        <div class="row mb-3">
                                            <div class="col-lg-12 col-xs-12">
                                             <asp:TextBox id="txtNo" runat="server" class="form-control" Visible="False"></asp:TextBox> 
                                                <asp:Label ID="Label25" runat="server" SkinID="errmsg" 
                            Text="Enter All details for cheque " Visible="False"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4" Visible="false" style="display:none;">
                                    <div class="form-group">
                                        <label>  <asp:Label ID="lblBankName" runat="server" Text="Bank Name" Width="124px" Visible="False"></asp:Label></label>
                                        <div class="row mb-3">
                                            <div class="col-lg-12 col-xs-12">
                                                
                                                
<asp:TextBox id="txtBankName" runat="server" CssClass="form-control" Visible="False"></asp:TextBox> 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4" Visible="false" style="display:none;">
                                    <div class="form-group">
                                        <label><asp:Label id="lblDate" runat="server" Visible="False"></asp:Label> </label>
                                        <div class="row mb-3">
                                            <div class="col-lg-12 col-xs-12">
                                               
                                                <asp:TextBox id="txtdate" runat="server" CssClass="form-control" Visible="False"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                 <!--  ===================================== -->

                             <div class="row mb-3" id="Onlinetran" runat="server" visible="false">
                               
                               
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Online Type</label>
                                       
                                          <asp:TextBox ID="txtOnlineType" runat="server" placeholder="Online Type"  TextMode="MultiLine" class="form-control" ></asp:TextBox>
                                    </div>
                                </div>
                                                              <div class="col-sm-4">
                                    <div class="form-group">
                                     <label>Transaction ID</label>
                                                               

 <asp:TextBox ID="txtOnlineTraansId" runat="server" placeholder="Online Trans ID"  TextMode="MultiLine" CssClass="form-control" ></asp:TextBox>
                                                    
                                    </div>
                                    </div>
                                                               
                                
                            </div>
                           

                                                         <div class="row mb-3">
                               
                               
                                <div class="col-sm-10">
                                    <div class="form-group">
                                        <label>Remark</label>
                                       
                                          <asp:TextBox ID="txtRemark" runat="server" placeholder="Enter Remark"  TextMode="MultiLine" CssClass="form-control" ></asp:TextBox>
                                    </div>
                                </div>
                                                              <div class="col-sm-2">
                                    <div class="form-group">
                                     <label>
                                                               

  <asp:CheckBox ID="isbth" Text="BTH" CssClass="form-check" runat="server"  AutoPostBack="True" 
                            oncheckedchanged="isbth_CheckedChanged" />
                                                    </label>
                                    </div>
                                    </div>
                                                               <div class="col-sm-4">
                                  
                                </div>
                                
                            </div>
                            <div class="row mb-3">
                                <div class="col-lg-12">
                                  
                                    <asp:Button ID="btnsave"  CssClass="btn btn-success" runat="server"   OnClientClick="this.disabled=true;" UseSubmitBehavior="false" Text="Save Bill" OnClick="btnsave_Click"  />
                                   
                                    <asp:Button ID="btnreport" runat="server"  OnClick="btnreport_Click" 
                                        Text=" Print" ToolTip="Print"  OnClientClick="this.disabled=true;" UseSubmitBehavior="false" Visible="false" CssClass="btn btn-info" />
                                    
                                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click1" 
                                        Text="Print" CssClass="btn btn-primary"  OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                                     <asp:HiddenField ID="hdfielddiscontFlag" runat="server" />
                        <asp:HiddenField ID="hdfielddiscont" runat="server" />
                        <asp:Label id="lblmsg" runat="server" SkinID="errmsg" Width="445px" Visible="False" Text="Amount should not greater than bill amount !!!"></asp:Label>
<asp:Label ID="Label12" runat="server" Text="Label" ForeColor="Green"  Width="445px" Visible="False" SkinID="errmsg"/>
                                </div>
                            </div>
                        </div>
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
    <script language="javascript" type="text/javascript">
        function Validate() {
            //alert("R");
            var radio = document.getElementsByName("RadioButtonList1"); //Client ID of the RadioButtonList1                          

            for (var i = 0; i < radio.length; i++) {

                if (radio[i].checked) { // Checked property to check radio Button check or not

                   // alert("Radio button having value " + radio[i].value + " was checked."); // Show the checked value

                    return true;

                }
                alert("Not checked any Payment mode");
                return false;
            }

        }    
   
   </script>
  
     </asp:Content>
