<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="EditClickSample.aspx.cs" Inherits="EditClickSample" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <style>
        #MainContent_Chkmaintestshort tbody tr td label {
        font-weight:500;
        width:100px;
        }
        .button-line .btn-blue{
        padding:5px 4px !important
        }
        .rounded_corners {
        max-height:500px;
            border-radius: 0px;
    border: none;
    background-color:rgba(56, 200, 221, 0.10980392156862745);
        overflow-y:scroll;
        }
        #MainContent_rblretecate tbody tr td {
        padding:0px 3px
        }
        .test-table tbody tr td a{
        color:#f10000 !important
        }
    </style>
             <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager><!-- Left side column. contains the logo and sidebar -->
           
          
           
         
            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
               
                <section class="content-header">
                    <h1>Edit Test Registration.</h1>
                    <ol class="breadcrumb">

                    
                        <li><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                       
                        <li class="active">Edit Test Registration.</li>
                    </ol>
              </section>

                <!-- Main content -->
                <section class="content">
                 <div class="row">
                  <div class="col-md-9">
                    <div class="box">                   
                        <form>                       
                            <div class="box-body">  
                                                 
                                <div class="row">                               
                                   <div class="col-md-4">
                                        <div class="form-group">
                                          
                                            <div class="row">
                                                <div class="col-md-6 col-xs-6">
                                                    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                                <ContentTemplate>
                                                   <asp:TextBox ID="txtCenter"  runat="server" class="form-control" placeholder="Select Center" Height="30px" OnTextChanged="ddlCenter_SelectedIndexChanged"
                                            AutoPostBack="True" style="width:125px"></asp:TextBox>
                                                    <div style="display: none; overflow: scroll;
                                                height: 100px" id="Div21">
                                            </div>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" 
                                            MinimumPrefixLength="1" TargetControlID="txtCenter"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ServiceMethod="GetCenter"
                                            CompletionListElementID="Div21">
                                        </cc1:AutoCompleteExtender>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-lg-6 col-xs-6">
                                                   <!-- <input type="text" class="form-control" id="" placeholder="Enter Name">-->
                                                    <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                                <ContentTemplate>
                                                      <asp:TextBox ID="txtSearchCardNo"  runat="server" class="form-control" 
                                                          placeholder="Search Card No" Height="30px"
                                            AutoPostBack="True" ontextchanged="txtSearchCardNo_TextChanged"></asp:TextBox>
                                          <div style="display: none; overflow: scroll; height: 100px; text-align: left"
                                   id="Div3">
                               </div>
                               <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                   CompletionListElementID="Div3" ServiceMethod="GetCardInfo"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" TargetControlID="txtSearchCardNo"
                                   MinimumPrefixLength="1">
                               </cc1:AutoCompleteExtender>
                                      </ContentTemplate>
                </asp:UpdatePanel> 
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="padding-left:0px;padding-right:0px">
                                            <asp:CheckBox ID="chkIsBH" runat="server" Text="BTH"/>
                                                       <asp:CheckBox ID="ChkEmergency" runat="server" Text="Emergency"/>
                                    </div>
                                   
                                   <div class="col-lg-3" style="padding-left:0px;padding-right:0px">
                                        <div class="form-group">
                                           
                                          <!--  <input type="text" class="form-control" id="" placeholder="Select Center"> -->
                                             <asp:RadioButtonList ID="rblretecate" Visible="false" Height="30px" runat="server"  
                RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="0">General</asp:ListItem>
          <asp:ListItem Value="1">Viber</asp:ListItem>
                <asp:ListItem Value="2">Whatsapp</asp:ListItem>
            </asp:RadioButtonList>    
            <asp:RadioButtonList ID="RadioButtonList1" Visible="false" Height="30px" runat="server"  
                RepeatDirection="Horizontal" style="display:none">
                <asp:ListItem Selected="True" Value="0">General</asp:ListItem>
      
            </asp:RadioButtonList> 
                                              </div>
                                       </div>
                                     <div class="col-lg-3" style="padding:0px">
                                        <div class="form-group">
            <asp:RadioButtonList ID="ReportBy"    runat="server"  
                RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="0">Print</asp:ListItem>
          <asp:ListItem Value="1">Email</asp:ListItem>
                <asp:ListItem Value="2">Both</asp:ListItem>
            </asp:RadioButtonList>       
            
                                       <!-- <asp:ListItem Value="1">Insurance</asp:ListItem>
                <asp:ListItem Value="2">Corporate</asp:ListItem> -->
                                        </div>
                                    </div>
                                    
                                    </div>
                                <div class="row">

                                    <div class="col-lg-6">
                                        <div class="form-group">
                                          
                                            <div class="row">
                                                <div class="col-lg-3 col-xs-4">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cmbInitial" TabIndex="0" Height="30px" runat="server" class="form-control"
                 OnSelectedIndexChanged="cmbInitial_SelectedIndexChanged" 
                 AutoPostBack="True">
                                    </asp:DropDownList>
                                      <asp:HiddenField ID="hdnstatus" runat="server" Value="0" />
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-lg-9 col-xs-8">
                                                   <!-- <input type="text" class="form-control" id="" placeholder="Enter Name">-->
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                       <asp:TextBox ID="txtFname" runat="server" placeholder="Enter Name" Height="30px" TabIndex="1" class="form-control" 
                 AutoPostBack="True" ontextchanged="txtFname_TextChanged"></asp:TextBox>  
                              
                                     <div style="display: none; overflow: scroll; width: 350px; height: 100px; text-align: left"
                                   id="Div19">
                               </div>
                               <cc1:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                   CompletionListElementID="Div19" ServiceMethod="GetPatientInfo"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" TargetControlID="txtFname"
                                   MinimumPrefixLength="1">
                               </cc1:AutoCompleteExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFname"
                                        Display="Dynamic" ErrorMessage="This field is required" SetFocusOnError="True"
                                        ValidationGroup="ValidateForm">
                                    </asp:RequiredFieldValidator>
                                      </ContentTemplate>
                </asp:UpdatePanel> 
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                           
                                            <div class="row">
                                                <div class="col-lg-12 col-xs-12">
                                                    <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlsex" runat="server" Height="30px" TabIndex="2" class="form-control">
            <asp:ListItem>Gender</asp:ListItem>
            <asp:ListItem>Male</asp:ListItem>
            <asp:ListItem>Female</asp:ListItem>
       </asp:DropDownList>
       </ContentTemplate>
       </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                           
                                            <div class="row">
                                                <div class="col-lg-6 col-xs-8">
                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtAge" runat="server" onkeypress="return NumberOnly()" placeholder="Enter Age" class="form-control" Height="30px"  AutoPostBack="True" ontextchanged="txtAge_TextChanged"  TabIndex="3" MaxLength="3" >
                                    </asp:TextBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-lg-6 col-xs-4">
                                                       <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                     <asp:DropDownList ID="cmdYMD" runat="server" Height="30px" TabIndex="4" class="form-control">
                                
                                <asp:ListItem>Year</asp:ListItem>
                                <asp:ListItem>Month</asp:ListItem>
                                <asp:ListItem>Day</asp:ListItem>
                            </asp:DropDownList>  
                            </ContentTemplate>
                            </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                <div class="row">
                                     <div class="col-lg-3">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                                <ContentTemplate>
                                                <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true"> 
                                                            <asp:TextBox ID="txtBirthdate" placeholder="Enter DOB" runat="server" 
                                                                Height="30px" class="form-control" 
                                                                TabIndex="5" MaxLength="15" AutoPostBack="True" 
                                                                ontextchanged="txtBirthdate_TextChanged" ></asp:TextBox>
                                                                 <span class="input-group-addon">
                                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                                </span>  
                                                                </div>

                                                                
                                                     </ContentTemplate>
                                                     </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                              </div>
                                              </div>
                                    <div class="col-lg-3">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                              <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txttelno" placeholder="Enter Mobile" MaxLength="12" 
                                                                runat="server"  Height="30px" class="form-control" 
                                                                TabIndex="6" AutoPostBack="True" ontextchanged="txttelno_TextChanged" ></asp:TextBox>
                                                               
                                                     </ContentTemplate>
                                                     </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                    
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txtemail" placeholder="Enter Email" runat="server"  
                                                                Height="30px" TabIndex="7" class="form-control" AutoPostBack="True" 
                                                                ontextchanged="txtemail_TextChanged"></asp:TextBox>
                                                      </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                  
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                           <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                              <asp:TextBox ID="txt_address" runat="server" placeholder="Enter address" TabIndex="8" Height="30px" TextMode="MultiLine" class="form-control">
                            </asp:TextBox>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                    </div>
                                <div class="row">
                                            <div id="Div1" class="col-lg-6"  runat="server" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                           
                                                            <asp:TextBox ID="txtpanno" placeholder="Enter Pan No" runat="server" Height="50px" class="form-control" TextMode="MultiLine" >
                                    </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div2" class="col-lg-6"  runat="server" visible="false">
                                                <div class="form-group">
                                                    
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                           
                                                            <asp:TextBox ID="txt_clinicalhistory" placeholder="Enter clinical history" runat="server" Height="50px" class="form-control"  TextMode="MultiLine" >
                                    </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                    </div>
                                <div class="row">
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                          <!--  <input type="text" class="form-control" id="" placeholder="Enter Doctor Name">-->
                                                            <asp:TextBox ID="txtDoctorName" placeholder="Ref Doctor" Height="30px" TabIndex="9"  runat="server" class="form-control"
                                                        ontextchanged="txtDoctorName_TextChanged"></asp:TextBox> 
                                                         <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: right"
                                                        id="Divdoc">
                                                    </div>
                                                     <cc1:autocompleteextender ID="AutoCompleteExtender3" runat="server"
                                        CompletionListElementID="Divdoc" ServiceMethod="FillDoctor" TargetControlID="txtDoctorName"
                                        MinimumPrefixLength="1">
                                    </cc1:autocompleteextender>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                              <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txt_remark" placeholder="" TabIndex="10" Height="30px" runat="server" class="form-control"  >
                                    </asp:TextBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                               

 <asp:TextBox  ID="txtpatcardno" placeholder="Patient Card No" class="form-control" runat="server" ></asp:TextBox>



                                            </div>
                                    <div class="col-md-3">
                                        <asp:TextBox  ID="txtcardexpdate" placeholder="Card Exp" class="form-control"  runat="server" ></asp:TextBox>
                                    </div>
                                            <div id="Div30" class="col-md-1" runat="server" style="display:none" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                            
                                                            <asp:TextBox ID="txtstate" placeholder="Enter State" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                    </div>
                                <div class="row">
                                            <div id="Div4" class="col-lg-4" runat="server" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                            
                                                            
        <asp:TextBox ID="txtDistrict" runat="server" placeholder="Enter District" class="form-control">
                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div5" class="col-lg-4" runat="server" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                          
                                                             <asp:TextBox ID="txtcity" placeholder="Enter city/Village" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                    <div class="col-md-3"></div>
                                           </div>
                                           <!-------------------------------------------------------------->
                                    <div class="row">
                                             <div class="col-md-3"  runat="server" id="D1" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                              <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txtWeight" placeholder="Enter Weight" TabIndex="100" runat="server" Height="30px" class="form-control" 
                                                               MaxLength="15" ></asp:TextBox>
                                                     </ContentTemplate>
                                                     </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                          <div class="col-lg-3" runat="server" id="D2" visible="false">
                                                <div class="form-group">
                                                    
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txtHeight" placeholder="Enter Height" TabIndex="110" runat="server" Height="30px"  class="form-control">     </asp:TextBox>
                                                      </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                                                                    <div class="col-md-3" runat="server" id="D3" visible="false">
                                                <div class="form-group">
                                                  
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                           <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                <ContentTemplate>
                                                              <asp:TextBox ID="txtLastPeriod" runat="server" TabIndex="120" placeholder="Enter LastPeriod"  Height="30px" TextMode="MultiLine" class="form-control">
                            </asp:TextBox>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                          <div class="col-lg-3" runat="server" id="D4" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                              <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                                <ContentTemplate>
                                                            
                                                                 <asp:TextBox ID="txtTherapy" runat="server" TabIndex="130" placeholder="Enter Therapy"  Height="30px" TextMode="MultiLine" class="form-control">
                            </asp:TextBox>
                                                     </ContentTemplate>
                                                     </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                    </div>
                                    <div class="row">
                                         <div class="col-lg-3" runat="server" id="D5" visible="false">
                                                <div class="form-group">
                                                    
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txtFSTime" placeholder="Enter FSTime" TabIndex="140" runat="server" Height="30px" class="form-control">     </asp:TextBox>
                                                      </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                         <div class="col-lg-3" runat="server" id="D6" visible="false">
                                                <div class="form-group">
                                                  
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                           <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                <ContentTemplate>
                                                             
                             <asp:TextBox ID="txtDieses" placeholder="Enter Disease" TabIndex="150" runat="server" TextMode="MultiLine" Height="45px" class="form-control" 
                                                                MaxLength="550" ></asp:TextBox>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                                                                      <div class="col-lg-6" runat="server" id="D7" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                              <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txtSymptoms" placeholder="Enter Symptoms"  TabIndex="160" runat="server" TextMode="MultiLine" Height="45px" class="form-control" 
                                                                ></asp:TextBox>
                                                     </ContentTemplate>
                                                     </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                    </div>

                                          


                                            
                                           
                                           

                                     <!-------------------------------------------------------------------->
                                <div class="row">
                                    <div class="col-lg-6">
                                        
                                                          
                                                              <asp:TextBox ID="txttests" placeholder="Add Test" runat="server" TabIndex="11" class="form-control" 
                                        AutoPostBack="True" OnTextChanged="txttests_TextChanged"></asp:TextBox>
                                    <div style="display: none; overflow: scroll; width: 348px; height: 120px" id="div">
                                    </div>
                                    <cc1:autocompleteextender ID="AutoCompleteExtender1" runat="server" 
                                        CompletionListElementID="div" ServiceMethod="FillTests" TargetControlID="txttests"
                                        MinimumPrefixLength="1">
                                    </cc1:autocompleteextender>
                                
                                                <div class="form-group text-package">
                                                  
                                                    

                                                      <div class="table-responsive" style="width:100%;margin-bottom:10px; max-height: 400px; vertical-align:top;  overflow:scroll ;  "  >
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                    <asp:GridView ID="grdTests" runat="server" class="table table-responsive table-sm table-bordered test-table" HeaderStyle-ForeColor="Black"
                                        AlternatingRowStyle-BackColor="White"  
                                            Width="100%"  PageSize="50" OnRowDataBound="grdTests_RowDataBound" OnRowDeleting="grdTests_RowDeleting" 
                                        DataKeyNames="MTCode" CellPadding="4" AutoGenerateDeleteButton="True" 
                                        AutoGenerateColumns="False" >
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="MTCode" HeaderText=" Code"></asp:BoundField>
                                            <asp:BoundField DataField="Maintestname" HeaderText="Test"></asp:BoundField>
                                             <asp:BoundField DataField="Type" HeaderText="Test/Package"></asp:BoundField>
                                            <asp:BoundField DataField="Amount" HeaderText="Charges"></asp:BoundField>
                                           
                                              <asp:TemplateField HeaderText="Disc">
                                            <ItemTemplate>
                                            <asp:TextBox ID="txtTestprofdiscount" Width="40px" AutoPostBack="true" Enabled="false" runat="server" 
                                                    ontextchanged="txtTestprofdiscount_TextChanged"></asp:TextBox>    
                                              </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Barcode" Visible="false">
                                            <ItemTemplate>
                                            <asp:TextBox ID="txtBarcode" Width="90px" AutoPostBack="false" Visible="false" runat="server" >
                                                   </asp:TextBox>    
                                              </ItemTemplate>
                                        </asp:TemplateField>
                                        </Columns>
                                        

                                        <HeaderStyle ForeColor="Black" />
                                        

                                    </asp:GridView>
                                  

                                    <asp:Label Style="position: relative" ID="lblAmtText" runat="server" Visible="False"
                                        >Total Amount = </asp:Label>&nbsp;
                                    <asp:Label Style="position: relative" ID="lblTotTestAmt" runat="server" Visible="False"
                                       >0</asp:Label>
                                 </ContentTemplate>
                                 </asp:UpdatePanel>
                                    
                            </div>
                           


                                                </div>
                                              
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group" style="background-color: #f7f7f7;
    padding: 10px;">
                                                   
                                                 
                                                     <div class="row form-group">
                                                          <div class="col-md-2">
                                                       <p class="label-form">  Prev Rec</p>
                                                     
                                                       </div>
                                                       <div class="col-md-3" style="margin-top:-5px">
                                                      <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                                      <ContentTemplate>                                                     
                                                     
                                                      <asp:TextBox ID="txtprevreceive" runat="server" style="    font-size: 17px;
    font-weight: 700;" Width="100%"  class="form-control" Text=" " Enabled="False"></asp:TextBox>
                                                       </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                     
                                                       </div>
                                                          <div class="col-md-3">
                                                       <p class="label-form">  Tot Amount</p>
                                                     
                                                       </div>
                                                           <div class="col-md-3">
                                                        <asp:UpdatePanel ID="tp" runat="server">
                                                      <ContentTemplate>                                                     
                                                     
                                                      <asp:TextBox ID="lbltotalpayment" runat="server" style="    font-size: 17px;
    font-weight: 700;" Width="100%"  class="form-control" Text=" " Enabled="False"></asp:TextBox>
                                                       </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                     
                                                       </div>
                                                   </div>
                                                      <div class="row form-group">
                                                       <div class="col-md-3">
                                                              <asp:Label ID="lbloot" runat="server"  class="form-control" Text=" Other Charge" Enabled="False"></asp:Label>
                                                       </div>
                                                          <div class="col-md-4">
                                                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                 <ContentTemplate>
                                                 <asp:TextBox ID="txtotherAmt" class="form-control" Height="30px"  runat="server" TabIndex="13" Text="0" onkeyup="GetPaidAmount1()" ontextchanged="txtotherAmt_TextChanged"  AutoPostBack="True" ></asp:TextBox>
                                              </ContentTemplate>
                                                 </asp:UpdatePanel>
                                                                   </div>
                                                          <div class="col-md-5">
                                                                 <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                                 <ContentTemplate>
                                                        <asp:TextBox ID="otherchargeRemark"  class="form-control" 
                                                              placeholder="Other charges Remark" runat="server" AutoPostBack="True" 
                                                              ontextchanged="otherchargeRemark_TextChanged"></asp:TextBox>
                                                              </ContentTemplate>
                                                              </asp:UpdatePanel>
                                                          </div>
                                                   </div>
                                                    <div class="row" style="margin-bottom:10px">
                                                                 <div class="col-md-4" style="display:none">
                                                             <label>Disc Type *</label>
                                                               <asp:UpdatePanel ID="M" runat="server">
                                                <ContentTemplate>
                                               
                                               <asp:RadioButtonList ID="Rbldisctype" TabIndex="14" class="form-control" runat="server" 
                RepeatDirection="Horizontal" AutoPostBack="True"  
                 onselectedindexchanged="Rbldisctype_SelectedIndexChanged">
                
                <asp:ListItem Selected="True" Value="Amt">Amt</asp:ListItem>
               
                
                   
            </asp:RadioButtonList>
             
                                                </ContentTemplate>
                                                
                                                </asp:UpdatePanel>
                                                             <label style="width: 100px;"> Tax </label>
                                                        </div>
                                                        <div class="col-md-4">
                                                              
                                                            <p class="label-form">  Disc Amt*</p>
                                               
                                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                   <asp:TextBox ID="txtDisamount" runat="server" TabIndex="150" placeholder="Discount" Height="30px" class="form-control" Text="0" AutoPostBack="True" 
                 ontextchanged="txtDisamount_TextChanged"></asp:TextBox>
                                                        </ContentTemplate>
                                                 </asp:UpdatePanel>
                                                        </div>

                                                        <div class="col-md-4">
                                                             <asp:UpdatePanel ID="UpdatePanel26" runat="server" style="">
                                                 <ContentTemplate>
                          <asp:RadioButtonList ID="RblDiscgiven" Visible="false"  runat="server" 
                RepeatDirection="Horizontal" AutoPostBack="True"  
                 TabIndex="12" onselectedindexchanged="RblDiscgiven_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="1" Text="Lab"></asp:ListItem>
              
                 <asp:ListItem Value="2" Text="Dr"></asp:ListItem>
                <asp:ListItem Value="3" Text="Both"></asp:ListItem>
                 
            </asp:RadioButtonList>
            </ContentTemplate>
            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="col-md-4">
                                                                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                                 <ContentTemplate>
                          <asp:TextBox ID="txtDisLabgiven" Visible="false" class="form-control" placeholder="Dis Lab" 
                                                         runat="server" AutoPostBack="True" ontextchanged="txtDisLabgiven_TextChanged"></asp:TextBox>
                          </ContentTemplate>
                          </asp:UpdatePanel>
                                                        <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                 <ContentTemplate>
                          <asp:TextBox ID="txtDisdrgiven" Visible="false" class="form-control" placeholder="Dis Dr"  
                                                         runat="server" AutoPostBack="True" ontextchanged="txtDisdrgiven_TextChanged"></asp:TextBox>
                         </ContentTemplate>
                         </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                       <div class="col-md-3">
                                                          Payment Type
                                                       </div>
                                                         <div class="col-md-5" style="margin-top:-4px">
                                                             <asp:RadioButtonList ID="rblPaymenttype"  runat="server" 
                RepeatDirection="Horizontal" AutoPostBack="True"  
                onselectedindexchanged="rblPaymenttype_SelectedIndexChanged" TabIndex="12">
                <asp:ListItem> Cash</asp:ListItem>
              
                 <asp:ListItem> Online </asp:ListItem>
                <asp:ListItem> Card</asp:ListItem>
                 
            </asp:RadioButtonList>
                                                       </div>
                                                        <div class="col-md-4">
                                                              <asp:TextBox ID="txtcardnumber" class="form-control" placeholder="Card Number" runat="server"></asp:TextBox>
                                                     
                                                            </div>
                                                        </div>

                                                  
                                                    <div class="row form-group" style="margin-top:10px">
                                                       
                                                      
                                                            <div class="col-md-3">
                                                             <p class="label-form"> Paid Amt *</p>
                                                        </div>
                                                          <div class="col-md-3">
                                                                 <asp:UpdatePanel ID="pa" runat="server">
                                                 <ContentTemplate>
                                                 <asp:TextBox ID="txtpaidamount" class="form-control" TabIndex="15" runat="server" onkeyup="GetPaidAmount1()" ontextchanged="txtpaidamount_TextChanged" AutoPostBack="True" ></asp:TextBox>
                                              </ContentTemplate>
                                                 </asp:UpdatePanel>
                                                        </div>
                                                         <div class="col-md-3">
                                                          <p class="label-form">   Balance  *</p>
                                                        </div>
                                                         <div class="col-md-3">
                                                                 <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                 <ContentTemplate>
                                                <asp:TextBox ID="txtBalance" style="    font-size: 17px;
    font-weight: 700;" class="form-control" Width="100%" placeholder="Balance" Enabled="False" runat="server"></asp:TextBox>
                                                </ContentTemplate>
                                                 </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="row" style="display:none">
                                                       
                                                        <div class="col-md-8">
                                                             <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                                      <ContentTemplate>
                                                       <asp:TextBox ID="txthstamount" class="form-control" Enabled="False" Width="50px" runat="server" ></asp:TextBox>
                                                       </ContentTemplate>
                                                       </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                    <div class="row form-group">
                                                    
                                                       
                                                        
                                                        <div class="col-md-3">
                                                             <p class="label-form">  Remark  *</p>
                                                        </div>
                                                        <div class="col-md-7">
                                                             <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                                <ContentTemplate>
                          <asp:TextBox ID="txtdiscountremark" class="form-control" Width="100%" placeholder="Discount Remark"  TextMode="MultiLine"  runat="server"></asp:TextBox>
                          </ContentTemplate>
                          </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        
                                                     
                                                    </div>
                                                   
                                                    
                                                      <table style="display:none">
                                                      <tr>
                                                      <td colspan="4">
                                                      
                                                      </td>
                                                      </tr>
                                                      <tr>
                                                      <td colspan="12">
                                                      
                                             
                                      
                                                      </td>
                                                      </tr>
                                                      <tr>
                                                      <td >
                                                     
                                                      </td>
                                                      
                                                        <td colspan="2">
                                                       
                                                   
                                                      </td>
                                                      <td>
                                                   
                                                      </td>
                                                      <td>
                                                      
                                                      </td>
                                                      </tr>
                                                      <tr>
                                                      <td>
                                                      
                                                   
                                                      </td>
                                                      <td>
                                                       

                                                      
                                                      </td>
                                                       <td id="CCard" colspan="2" runat="server" visible="false">
                                               
                                                           
                                                </td>
                                                     
                                                      </tr>
                                                     
                                                      <tr >
                                                      <td>
                                                      </td>
                                                      <td id="Td1" runat="server">
                                                     
                                                      </td>

                                                      </tr>
                                                       <tr>
                                                      <td>
                                                      
                                                      </td>
                                                       <td>
                                                
                                                      </td>
                                                       <td>
                                                     
                                                      </td>
                                                       <td>
                                                   
                                                    

                                                      </td>
                                                      </tr>
                                                       <tr id="Tr1" runat="server" visible="false">
                          <td >
                        
                         </td>
                          <td colspan="3">
                       
                         </td>
                         </tr>
                         <tr>
                         <td >
                        
                         </td>
                          <td >
                      
                         </td>
                         <td colspan="2"  runat="server" id="Dg" >
                        
           
                         </td>
                          
                         </tr>
                        
                         <tr>
                         <td >
                        
                         </td>
                          <td >
                     
                         </td>
                         <td>
                          
                         </td>
                         <td>
                         
                         </td>
                         </tr>
                          <tr id ="disrema" runat="server" >
                          <td>
                         
                          </td>
                         <td colspan="3">
                         
                         </td>
                         </tr>
                           </table>
                                                </div>
                                                <div class="row">
                                                         <div class="col-md-12 button-line">
                                                               <asp:Button ID="btnadd"  OnClick="btnadd_Click" class=" btn-blue" style="padding: 4px 10px;
    border-radius: 0px;
    font-size: 12px;" runat="server"       Text="Clear"></asp:Button>
                                                             <asp:UpdatePanel ID="UpdatePanel23" runat="server" style="display:inline-block">
            <ContentTemplate>
                <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
               
                 <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" class=" btn-blue" runat="server" Text="Save" OnClientClick="return Validate();" >
                                        </asp:Button>
            </ContentTemplate>
        </asp:UpdatePanel>
                                                              <asp:Button ID="BtnReceipt" TabIndex="16"  class=" btn-blue" runat="server" 
                                                         Text="Save &amp; Bill" onclick="BtnReceipt_Click"   >
                                        </asp:Button>
                                                              <asp:Button ID="btnbarcodeEntry" runat="server" Text="Dept Barcode"  
                                                         class=" btn-blue" onclick="btnbarcodeEntry_Click"></asp:Button>
                                                               <asp:Button ID="btnpatientcard"  class=" btn-blue" runat="server" 
                                                         Text="Card" onclick="btnpatientcard_Click"    ></asp:Button>
                                                                <asp:Button ID="btnbprint" runat="server" Text="Sample Barcode"  
                                                         class=" btn-blue" onclick="btnbprint_Click" ></asp:Button>
                                                                </div>
                                                         </div>
                                                <br />
                                                <div class="row">
                                                    <div class="col-md-12">
                                                         <asp:Label ID="LblbrowsePres" Text="Upload Prescription:" Font-Bold="true" runat="server" ></asp:Label> 
                             </br>  
                                                        <asp:HyperLink ID="Hyp_viewPres" runat="server" NavigateUrl='<%# Eval("UploadPrescription") %>'>View Pres</asp:HyperLink>
                           

                                                         </br>   
                                                        <table>
                             <tr>
                             <td>
                             <asp:FileUpload ID="FUBrowsePresc" runat="server"></asp:FileUpload>
                              <asp:Label ID="LblFilename" Text="" Font-Bold="true" runat="server"></asp:Label> 
                             </td>
                             <td>
                             <asp:Button ID="btnupload" runat="server" Textclass="btn btn-primary" runat="server" 
                                                         Text="Upload" class="btn-blue" Visible="False" ></asp:Button>
                             </td>
                             </tr>
                             </table>

                                                    </div>
                                                </div>
                                                       
                                            </div>
                                            
                                             </div>
                                <div class="row">      
                                                                              <div class="col-lg-6">
                                               
                                                <div class="form-group" >
                                                 <div class="rounded_corners" style="width:100%; height: 1px; vertical-align:top;  overflow:scroll ; visibility:hidden  " >
                           <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                    <asp:GridView ID="grdPackage" runat="server"    HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   Width="100%"
                                        PageSize="50" OnRowDataBound="grdPackage_RowDataBound"  AutoGenerateDeleteButton="false" 
                                        AutoGenerateColumns="False" DataKeyNames="STCODE" onrowdeleting="grdPackage_RowDeleting">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="SampleType" HeaderText="SampleType"></asp:BoundField>
                                            <asp:BoundField DataField="STCODE" HeaderText="Test Codes"></asp:BoundField>
                                            <asp:BoundField DataField="TestName" HeaderText="Test Names"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Barcode No">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtbarcodeid" runat="server"></asp:TextBox>                                                    
                                                 <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtbarcodeid"
                                                              FilterMode="ValidChars" ValidChars="A,B,C,D,E,F,E,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,0,1,2,3,4,5,6,7,8,9">
                                                      </cc1:FilteredTextBoxExtender>    
                                                 <asp:Label id="lblError" runat="server" SkinID="errmsg" ></asp:Label>
                                                 <asp:Label id="lblRequiredField" runat="server" Visible="false" SkinID="errmsg"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:BoundField DataField="Testcharges" Visible="false" HeaderText="Testcharges"></asp:BoundField>
                                           
                                             <asp:TemplateField HeaderText="Disc" Visible="false">
                                            <ItemTemplate>
                                            <asp:TextBox ID="txtTestdiscount" Width="50px" runat="server" AutoPostBack="True" 
                                                    ontextchanged="txtTestdiscount_TextChanged"></asp:TextBox>    
                                              </ItemTemplate>
                                        </asp:TemplateField>
                                        </Columns>
                                      

                                        <HeaderStyle ForeColor="Black" />
                                      

                                    </asp:GridView>
                            </ContentTemplate>
                              </asp:UpdatePanel> 
                           
                           </div>
                        
                                                </div>
                                                                                  </div>
                                    </div>


                                    <div class="row">
                                               

                                                  <div class="col-lg-12">
                                                <div class="form-group" >
                                                <table id="Table1" width="100%" runat="server">
                                               
                                                 <tr id="CCheq" runat="server" visible="false">
                                                <td>
                                               <label>Cheque Date *</label>
                                                </td>
                                                 <td>
                                                <label>Cheque No *</label>
                                                </td>
                                                 <td>
                                                <label>Bank Name *</label>
                                                </td>
                                                 <td>
                                              
                                                </td>
                                                </tr>
                                                 <tr id="CCheq1" runat="server" visible="false">
                                                <td>
                                                <table>
                                                <tr  >
                                                <td>
                                                <asp:TextBox ID="txtchequedate" runat="server" class="form-control" ></asp:TextBox>
                                                </td>
                                                <td>
                                                 <cc1:calendarextender ID="CalendarExtender3"  Format="dd/MM/yyyy" runat="server" TargetControlID="txtchequedate"
                            PopupButtonID="ImageButton3">
                        </cc1:calendarextender>
                         <asp:ImageButton runat="Server" ID="ImageButton3" ImageUrl="~/Images/Calendar_scheduleHS.png"
                                                AlternateText="Click to show calendar"  />
                                                </td>
                                                </tr>
                                                </table>
                                              
       
                                                </td>
                                                 <td>
                                                  <asp:TextBox ID="txtchequenumber" class="form-control" placeholder="Cheque Number" runat="server"></asp:TextBox>
                                                </td>
                                                 <td>
                                                
                                                  <asp:TextBox ID="txtbankname" class="form-control" placeholder="Bank Name" runat="server"></asp:TextBox>
                                                </td>
                                                 <td>
                                              
                                                </td>
                                                </tr>
                                                
                                                 <tr id="CCard1" runat="server" visible="false">
                                               <td>
                                               </td>
                                                 <td>
                                               
                                                </td>
                                                 <td>
                                              
                                                </td>
                                                 <td>
                                              
                                                </td>
                                                </tr>
                                                 <tr>
                                                       <td align="left">
                                                  <asp:Button ID="btnresultentry"  class="btn btn-blue" runat="server" 
                                                         Text="Result " onclick="btnresultentry_Click" Visible="False"  >
                                        </asp:Button>
                                                 </td>
                                                 
                                                 </tr>
                                                 <tr>
                                                 <td colspan="4" align="center">
                                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" BorderColor="Green" ForeColor="Red" Font-Bold="true" runat="server"  
ControlToValidate="txttelno" ErrorMessage="Invalid Mobile no"  
ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator> 
<asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtemail" BorderColor="Green" ForeColor="Red" Font-Bold="true"  ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>
                                                 <asp:Label  ID="Label10" runat="server" ForeColor="Green" Font-Bold="true"  ></asp:Label>
                                                 </td>
                                                 </tr>
                                                </table>
                                                     
                                                     
                                               
                                               
                                               
                                                </div>
                                                </div>

                                           </div>
                                           </div>

                                          
                                          </div>
                                        </div>

                                                               <div class="col-md-3" runat="server" id="Shortcut">
                           <div class="box">
                                
                               <div class="box-body" style="padding:0px">
                                    <div class="form-group"> 
                                      
                    
                   
                                      <div class="rounded_corners table-right" style="width:100%; height: 100%; vertical-align:top;   "  > 
                            
                           
<asp:CheckBoxList ID="Chkmaintestshort" runat="server" RepeatColumns="2"   Width="100%" AutoPostBack="True" 
                                    onselectedindexchanged="Chkmaintestshort_SelectedIndexChanged" ></asp:CheckBoxList>

                              </div>
 
                                   </div> 
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

