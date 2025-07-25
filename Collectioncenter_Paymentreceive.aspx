 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="Collectioncenter_Paymentreceive.aspx.cs" Inherits="Collectioncenter_Paymentreceive" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Center Payment Receive</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Center Payment Receive</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-2">
                                <div class="col-sm-3" style="display:none">
                                    <div class="form-group">
                                       
                                        
                                         <asp:dropdownlist id="ddlfyear" runat="server" class="form-control form-select">
                </asp:dropdownlist>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                      
                                        <asp:TextBox id="TxtCenter"  placeholder="Center" AutoPostBack="true" runat="server" class="form-control" OnTextChanged="TxtCenter_TextChanged"></asp:TextBox>
                                        <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 220px; HEIGHT: 100px" id="Div2"></DIV>
                                        <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="Div2" ServiceMethod="FillCenter" 
                                            TargetControlID="TxtCenter" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                  <div class="col-sm-2">
                                    <div class="form-group">
                                    
                                          <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true"> 
                                                             <asp:TextBox ID="txtdatecur" class="form-control" runat="server"></asp:TextBox>
                                                                 <span class="input-group-addon">
                                                                   <i class="fa fa-calendar"></i>
                                                                </span>  
                                                                </div>
                                                  
                                          <cc1:CalendarExtender ID="CalendarExtender2"  Format="dd/MM/yyyy" runat="server" TargetControlID="txtdatecur"
                            PopupButtonID="ImageButton4">
                        </cc1:CalendarExtender>
                                                
                         <div style="display:none"> <asp:ImageButton runat="Server" ID="ImageButton1" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                                AlternateText="Click to show calendar"  />         </div>
                                    </div>
                                    </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                        <asp:button id="btnshow" OnClientClick="return validate();" runat="server" text="Click" class="btn btn-primary" onclick="btnshow_Click"  />
                                        
                                          <asp:Button ID="btnreport" runat="server" class="btn btn-warning" onclick="btnreport_Click" Text="Report" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-2">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        <div class="radio1">
                                          
                                              
                                                    <label>
                                                        <asp:RadioButtonList ID="rblpaymenttype" AutoPostBack="true" runat="server" 
                   RepeatDirection="Horizontal" CssClass="form-check"
                   onselectedindexchanged="rblpaymenttype_SelectedIndexChanged">
                   <asp:ListItem Selected="True">Cash</asp:ListItem>
                   <asp:ListItem>Cheque</asp:ListItem>
               </asp:RadioButtonList>
                                                    </label>
                                                 
                                            
                                            </div>
                                      
                                    </div>
                                </div>
                                <div class="col-sm-3"  id="tt1" runat="server" visible="false">
                                    <div class="form-group">
                                      
                                       
                                         <asp:label id="Lbltotalbillamount" runat="server" class="form-control" placeholder="Total Bill Amount"></asp:label>
                                    </div>
                                </div>
                                <div class="col-sm-3"  id="tt" runat="server" visible="false">
                                    <div class="form-group">
                                       
                                   
                                        <asp:label id="Lblpaidamount" runat="server" class="form-control" placeholder="Paid Amount"></asp:label>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    Discount
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       
                                         <asp:TextBox ID="txtdiscount" runat="server" class="form-control" 
                                             placeholder="Discount" AutoPostBack="True" 
                                             ontextchanged="txtdiscount_TextChanged"></asp:TextBox>
                                    </div>
                                </div>

                              </div>
                             <div class="row mb-2">
                                <div class="col-sm-3" runat="server" visible="false" id="CN">
                                    <div class="form-group">
                                       
                                        
                                        <asp:TextBox id="txtchequeNo" runat="server" class="form-control" placeholder="Cheque No"></asp:TextBox> 
                                    </div>
                                </div>
                                <div class="col-sm-3" runat="server" visible="false" id="BN">
                                    <div class="form-group">
                                        
                                      
                                        <asp:TextBox id="txtBankName" runat="server" class="form-control" placeholder="Bank Name" ></asp:TextBox> 
                                    </div>
                                </div>
                                <div class="col-sm-4" runat="server" visible="false" id="CH">
                                    <div class="form-group d-flex flex-row">
                                       
                                       
                                         <asp:TextBox ID="txtchequedate" CssClass="form-control" runat="server"></asp:TextBox>
                                          <cc1:CalendarExtender ID="CalendarExtender3"  Format="dd/MM/yyyy" runat="server" TargetControlID="txtchequedate"
                            PopupButtonID="ImageButton3">
                        </cc1:CalendarExtender>
                         <asp:ImageButton runat="Server" ID="ImageButton3" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                                AlternateText="Click to show calendar"  />
                                    </div>
                                </div>

                                </div>
                            <div class="row mb-2">

                                <div class="col-sm-1">
                                    Rec Amt
                        </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                      
                                       
                                        <asp:TextBox ID="txtrecamount"  runat="server" AutoPostBack="True"  
                                            class="form-control" placeholder="Amount Received" 
                                            ontextchanged="txtrecamount_TextChanged"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    Balance
                                    </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        
                                        <asp:TextBox ID="txtbalanceamount" runat="server" class="form-control" placeholder="Balance Amount"></asp:TextBox>
                                    </div>
                                </div>
                                </div>
                            <div class="row mb-2">
                                 <div class="col-sm-1">
                                     Date
                                     </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                                                 <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true"> 
                                                             <asp:TextBox ID="txtentrydate" class="form-control" runat="server"></asp:TextBox>
                                                                 <span class="input-group-addon">
                                                                     <i class="fa fa-calendar"></i>
                                                                </span>  
                                                                </div>
                                                  
                                          <cc1:CalendarExtender ID="CalendarExtender1"  Format="dd/MM/yyyy" runat="server" TargetControlID="txtentrydate"
                            PopupButtonID="ImageButton4">
                        </cc1:CalendarExtender>
                                                
                         <div style="display:none"> <asp:ImageButton runat="Server" ID="ImageButton4" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                                AlternateText="Click to show calendar"  />         </div>
                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    Remark
                                    </div>
                                    <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        
                                        <asp:TextBox ID="txtremark" placeholder="Enter Remark" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                   
                                    <asp:Button ID="btnsave" runat="server"  OnClientClick="return ValidateDelete();" class="btn btn-primary" onclick="btnsave_Click" Text="Save" />
                                     <asp:Label ID="LblMsg" runat="server" Text="" Font-Bold="True"   ForeColor="Red" Width="354px"></asp:Label>
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
                   function ValidateDelete() {
                       var Check = confirm('Are you sure you want to receive payment ?')
                       if (Check == true) {
                           return true;
                       }
                       else {
                           return false;
                       }
                   }
                
 </script> 

      </asp:Content>
