<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="BookAppoinment.aspx.cs" Inherits="BookAppoinment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
   
                <!-- Content Header (Page header) -->
               
                <section class="content-header">
                    <h1>Appoinment Registration</h1>
                    <ol class="breadcrumb">

                    
                        <li><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                       
                        <li class="active">Appoinment Registration</li>
                    </ol>
              </section>

                <!-- Main content -->
                <section class="content">
                 <div class="row">
                  <div class="col-lg-12">
                    <div class="box">                   
                        </div>
                            <div class="box-body">  
                                                 
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
                                      <asp:HiddenField ID="hdnstatus" runat="server" Value=0 />
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-lg-9 col-xs-8">
                                                   <!-- <input type="text" class="form-control" id="" placeholder="Enter Name">-->
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                       <asp:TextBox ID="txtFname" runat="server" placeholder="Enter Name" Height="30px" TabIndex="1" class="form-control" 
                 AutoPostBack="false"></asp:TextBox>  
                              
                                  
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
                                    
                                    <div class="col-lg-2">
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


                                     <div class="col-lg-2" runat="server" visible="false" id="Slo">
                                        <div class="form-group">
                                           
                                            <div class="row">
                                                <div class="col-lg-12 col-xs-12">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                        <asp:DropDownList ID ="ddlToTime" class="form-control" runat="server"></asp:DropDownList>

       </ContentTemplate>
       </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-2" runat="server" visible="false" id="Slo1">
                                        <div class="form-group">
                                           
                                            <div class="row">
                                                <div class="col-lg-12 col-xs-12">
                                                    
                                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            
                                            <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>
                                        </div>

      
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
                                                    <asp:TextBox ID="txtAge" runat="server" onkeypress="return NumberOnly()" placeholder="Age" class="form-control" Height="30px" TabIndex="3" MaxLength="3" >
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
                                    <div class="col-lg-3">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                              <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txttelno" placeholder="Enter Mobile" runat="server" Height="30px" class="form-control" 
                                                                TabIndex="5" MaxLength="15" ></asp:TextBox>
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
                                                            <asp:TextBox ID="txtemail" placeholder="Enter Email" runat="server" Height="30px" TabIndex="6" class="form-control">     </asp:TextBox>
                                                      </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                  
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                           <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                              <asp:TextBox ID="txt_address" runat="server" placeholder="Enter address" TabIndex="7" Height="30px" TextMode="MultiLine" class="form-control">
                            </asp:TextBox>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div3" class="col-lg-6"  runat="server" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                           
                                                            <asp:TextBox ID="txtpanno" placeholder="Enter Pan No" runat="server" Height="50px" class="form-control" TextMode="MultiLine" >
                                    </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div4" class="col-lg-6"  runat="server" visible="false">
                                                <div class="form-group">
                                                    
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                           
                                                            <asp:TextBox ID="txt_clinicalhistory" placeholder="Enter clinical history" runat="server" Height="50px" class="form-control"  TextMode="MultiLine" >
                                    </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                     <div id="Div5" class="col-lg-4" runat="server" >
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                            
                                                            <asp:TextBox ID="txtstate" placeholder="Enter State" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div6" class="col-lg-4" runat="server" >
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                            
                                                            
        <asp:TextBox ID="txtDistrict" runat="server" placeholder="Enter District" class="form-control">
                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div7" class="col-lg-4" runat="server" >
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                          
                                                             <asp:TextBox ID="txtcity" placeholder="Enter city/Village" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                          <!--  <input type="text" class="form-control" id="" placeholder="Enter Doctor Name">-->
                                                            <asp:TextBox ID="txtDoctorName" placeholder="Ref Doctor" Height="30px" TabIndex="8"  runat="server" class="form-control"
                                                        ontextchanged="txtDoctorName_TextChanged"></asp:TextBox> 
                                                         <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: right"
                                                        id="Divdoc">
                                                    </div>
                                                     <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                        CompletionListElementID="Divdoc" ServiceMethod="FillDoctor"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" TargetControlID="txtDoctorName"
                                        MinimumPrefixLength="1">
                                    </cc1:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                              <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txt_remark" placeholder="Note" TabIndex="9" Height="30px" runat="server" class="form-control"  >
                                    </asp:TextBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                           
                                           
                                           <!-------------------------------------------------------------->
                                                            
                                     <!-------------------------------------------------------------------->
                                     <%-- HospAdd ------------------- --%>
                                                            <div class="row" >
                                                                 <div id="Div1"  runat="server" class="col-md-4">
                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID ="ddlCollectionReceiver" class="form-control" runat="server"></asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div9"  runat="server" class="col-md-4">
                                                                    <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtadmitpatient" runat="server" TabIndex="20" placeholder="Name of hosp where patient is admit " Height="30px" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div10"  runat="server" class="col-md-4">
                                                                    <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txthospID" runat="server" TabIndex="20" placeholder="Enter patient Hosp ID " Height="30px" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                </div>
                                                              <%-- EndHospAdd ------------------- --%>
                                                             <%-- SympAdd ------------------- --%>
                                                            <div class="row" style="padding-bottom: 8px" runat="server" visible="false">
                                                                 <div id="Div11"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:CheckBox ID="ChkILI" Text="ILI" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div12"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                                                                        <ContentTemplate>
                                                                    <asp:CheckBox ID="ChlFever" Text="Fever" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div13"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                                                                        <ContentTemplate>
                                                                             <asp:TextBox ID="txtfeverduration" placeholder="Fever Duration " class="form-control" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div14"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                                                                        <ContentTemplate>
                                                                              <asp:CheckBox ID="chkcough" Text="Cough" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div15"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                                                                        <ContentTemplate>
                                                                              <asp:TextBox ID="txtcoughduration"  placeholder="Cough Duration " class="form-control" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div16"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                                                                        <ContentTemplate>
                                                                             <asp:CheckBox ID="chksari" Text="SARI" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                </div>
                                                              <%-- EndSympAdd ------------------- --%>
                                                            <div class="row" style="padding-bottom: 8px" runat="server" visible="false">
                                                                 <div id="Div17"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                                                        <ContentTemplate>
                                                                           <asp:TextBox ID="txtComorbidity"  placeholder="Co morbidity " class="form-control" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div18"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                                                                        <ContentTemplate>
                                                                           <asp:TextBox ID="txttempreco"  placeholder="Temp Recorded " class="form-control" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-md-1">
                                                                <p class="label-form">Sputum</p>
                                                            </div>
                                                                <div id="Div19"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                                                                        <ContentTemplate>
                                                                            
                                                                           <asp:RadioButtonList ID="ddlsputum" RepeatDirection="Horizontal" runat="server" Width="100px" TabIndex="7" class="form-control">

                                                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Selected="True" Value="No"></asp:ListItem>
                                                                                
                                                                            </asp:RadioButtonList>
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div20"  runat="server" class="col-md-5">
                                                                    <asp:UpdatePanel ID="UpdatePanel48" runat="server">
                                                                        <ContentTemplate>
                                                                           <asp:TextBox ID="txtadditionalsymptoms"  placeholder="Additional symptoms?if any  " class="form-control" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                </div>
                                                            <div class="row" style="padding-bottom: 8px" runat="server" visible="false">
                                                                 <div id="Div21"  runat="server" class="col-md-5">
                                                                    <asp:UpdatePanel ID="UpdatePanel49" runat="server">
                                                                        <ContentTemplate>
                                                                           <asp:Label ID="TextBox1"  Text="Travel history in last 14 days?"   Font-Bold="true" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div id="Div22"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel50" runat="server">
                                                                        <ContentTemplate>
                                                                            
                                                                           <asp:RadioButtonList ID="rblVisites" RepeatDirection="Horizontal" runat="server" Width="100px" TabIndex="7" class="form-control">

                                                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Selected="True" Value="No"></asp:ListItem>
                                                                                
                                                                            </asp:RadioButtonList>
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div23"  runat="server" class="col-md-5">
                                                                    <asp:UpdatePanel ID="UpdatePanel51" runat="server">
                                                                        <ContentTemplate>
                                                                           <asp:TextBox ID="txtcountryvisit"   placeholder="Country visited by you (if yes) " class="form-control" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                </div>
                                                             <div class="row" style="padding-bottom: 8px" runat="server" visible="false">
                                                                 <div id="Div24"  runat="server" class="col-md-5">
                                                                    <asp:UpdatePanel ID="UpdatePanel52" runat="server">
                                                                        <ContentTemplate>
                                                                           <asp:Label ID="Label2"  Text="Is the patient admited in isolation ward/unit in hospital?"   Font-Bold="true" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                  <div id="Div25"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel53" runat="server">
                                                                        <ContentTemplate>
                                                                            
                                                                           <asp:RadioButtonList ID="rblisolation" RepeatDirection="Horizontal" runat="server" Width="100px" TabIndex="7" class="form-control">

                                                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Selected="True" Value="No"></asp:ListItem>
                                                                                
                                                                            </asp:RadioButtonList>
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 </div>
                                            <div class="row">
                                            <div class="col-lg-12 text-center">
                                                
                                              
                                             
                                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                                                 <asp:Button ID="btnreset" runat="server" Text="Reset" />
                                               <asp:ImageButton ID="btnwhatapp" runat="server" Visible="false" ImageUrl="~/Images0/Whatapp.jpg" ImageAlign="AbsMiddle" OnClick="btnwhatapp_Click"  />
                                             
                                       <asp:Label  ID="Label10" runat="server" Font-Bold="true" ForeColor="Red"  ></asp:Label>
                                            </div>
                                        </div>

                                           </div>
                                           </div>

                                          
                                          </div>
                                        </div>
                                                              
                                       
                                       </section>
                                   
                                   
                                    
 <!--</form>
                            </div>
                </section>
                
            </div>-->
            <!-- /.content-wrapper -->
           
           
     
  
       
        <script type="text/javascript">
            //Date picker
            $('#fromdate, #todate').datepicker({
                autoclose: true
            })
        </script>
     <script language="javascript" type="text/javascript">
         function OpenReport() {
             window.open("Reports.aspx");
         }
               </script>
               
   
    
   

  
 
<script type="text/javascript">
    function NumberOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
            event.returnValue = true;
        else
            event.returnValue = false;
    }
    </script>
    </asp:Content>