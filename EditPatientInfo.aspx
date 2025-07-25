<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="EditPatientInfo.aspx.cs" Inherits="EditPatientInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
 <section class="content-header">
                    <h1>Patient Info Edit</h1>
                    <ol class="breadcrumb">
                    
                      
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Patient Info Edit</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                <div class="row">
                  <div class="col-lg-12">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-lg-3 col-xs-4">
                                               
                                                 <asp:DropDownList ID="cmbInitial"  runat="server" class="form-control" OnSelectedIndexChanged="cmbInitial_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-9 col-xs-8">
                                               
                                                  <asp:TextBox ID="txtFname" runat="server" class="form-control"  placeholder="Enter Name">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFname"
                                        Display="Dynamic" ErrorMessage="This field is required" SetFocusOnError="True"
                                        ValidationGroup="ValidateForm">
                                    </asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                      
                                        <div class="row">
                                            <div class="col-lg-12 col-xs-12">
                                                
                                                
<asp:TextBox id="txtsex"  runat="server" placeholder="Enter Gender" class="form-control"></asp:TextBox> 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-lg-6 col-xs-8">
                                              
                                                <asp:TextBox id="txtAge" runat="server" MaxLength="3" placeholder="Age" class="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAge"
        Display="Dynamic" ErrorMessage="This field is required" SetFocusOnError="True" ValidationGroup="ValidateForm" Font-Bold="True"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtAge"
        ErrorMessage="Please Enter Valid Age" Operator="DataTypeCheck" Style="position: relative"
        Type="Integer" ValidationGroup="ValidateForm" SetFocusOnError="True" Display="Dynamic" Font-Bold="True"></asp:CompareValidator>
                                            </div>
                                            <div class="col-lg-6 col-xs-4">
                                                
                                                 <asp:DropDownList id="cmdYMD" runat="server" class="form-control">
                <asp:ListItem>Year</asp:ListItem>
                <asp:ListItem>Month</asp:ListItem>
                <asp:ListItem>Day</asp:ListItem>
            </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-lg-12 col-xs-12">
                                                
                                                <asp:TextBox ID="txttelno" runat="server" class="form-control" placeholder="Enter Mobile"></asp:TextBox>
        <asp:Label ID="lbl_mobile" runat="server" Font-Bold="true" ForeColor="Red" Visible="false" SkinID="errmsg" ></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-lg-12 col-xs-12">
                                                
                                                  <asp:TextBox ID="txtemail" runat="server" class="form-control"  placeholder="Enter Email"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-lg-12 col-xs-12">
                                               
                                                 <asp:TextBox id="txtDoctorName" placeholder="Enter Ref Doctor" runat="server" class="form-control"  ></asp:TextBox>
                                                  <asp:Label ID="lblexistRefDoc" runat="server" Text=""></asp:Label>
                                                  <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: right"
                                                        id="Divdoc">
                                                    </div>
                                                       <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" 
                              MinimumPrefixLength="1" TargetControlID="txtDoctorName"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ServiceMethod="FillDoctor" 
                              CompletionListElementID="Divdoc" >
                              </cc1:AutoCompleteExtender> 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                     
                                       
                                        <asp:TextBox id="txt_address" runat="server" class="form-control"  placeholder="Enter Address" TextMode="MultiLine" TabIndex="10"></asp:TextBox>

                                    </div>
                                </div>
                                <div id="Div1" class="col-lg-4" runat="server" visible="false">
                                    <div class="form-group">
                                      
                                        
                                        <asp:TextBox id="txt_clinicalhistory" runat="server" class="form-control"  placeholder="Enter Patient Clinical History" TextMode="MultiLine" ></asp:TextBox>
 

                                    </div>
                                </div>
                                <div class="col-lg-8">
                                    <div class="form-group">
                                     
                                        
                                          <asp:TextBox id="txt_remark" runat="server"   TextMode="MultiLine" class="form-control"  placeholder="Enter Remark"></asp:TextBox>

                                    </div>
                                </div>


                                  <!-------------------------------------------------------------->

                                             <div class="col-lg-3"  runat="server" id="D1" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                              <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txtWeight" placeholder="Enter Weight" runat="server" Height="30px" class="form-control" 
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
                                                            <asp:TextBox ID="txtHeight" placeholder="Enter Height" runat="server" Height="30px"  class="form-control">     </asp:TextBox>
                                                      </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6" runat="server" id="D3" visible="false">
                                                <div class="form-group">
                                                  
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                           <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                                <ContentTemplate>
                                                              <asp:TextBox ID="txtLastPeriod" runat="server" placeholder="Enter LastPeriod"  Height="30px" TextMode="MultiLine" class="form-control">
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
                                                            <asp:TextBox ID="txtDieses" placeholder="Enter Disease" runat="server" Height="30px" class="form-control" 
                                                                MaxLength="15" ></asp:TextBox>
                                                     </ContentTemplate>
                                                     </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3" runat="server" id="D5" visible="false">
                                                <div class="form-group">
                                                    
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txtFSTime" placeholder="Enter FSTime" runat="server" Height="30px" class="form-control">     </asp:TextBox>
                                                      </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6" runat="server" id="D6" visible="false">
                                                <div class="form-group">
                                                  
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                           <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                <ContentTemplate>
                                                              <asp:TextBox ID="txtTherapy" runat="server" placeholder="Enter Therapy"  Height="30px" TextMode="MultiLine" class="form-control">
                            </asp:TextBox>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                              <div class="col-lg-12" runat="server" id="D7" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                              <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txtSymptoms" placeholder="Enter Symptoms" runat="server" TextMode="MultiLine" Height="30px" class="form-control" 
                                                                ></asp:TextBox>
                                                     </ContentTemplate>
                                                     </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-12" runat="server" id="D8" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                             <asp:RadioButtonList ID="rblretecate"  Height="40px" class="form-control" runat="server"  
                RepeatDirection="Horizontal">
                 <asp:ListItem Selected="True" Value="0">General</asp:ListItem>
                 <asp:ListItem Value="1">Viber</asp:ListItem>
                 <asp:ListItem Value="2">Whatsapp</asp:ListItem>
            </asp:RadioButtonList>    
             </ContentTemplate>
                                                     </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                     <!-------------------------------------------------------------------->



                               
                            </div>
                        </div>
                      





                    </div>
                   
                     <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                  <asp:Button ID="btnBack" runat="server" class="btn btn-primary" Text="Back" OnClick="btnBack_Click"  />
                                  
                                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" OnClientClick="return Validate();" 
                 OnClick="btnSubmit_Click" Text="Save" ValidationGroup="ValidateForm"      />

                                                         <asp:Button ID="btnpatientcard"  class="btn btn-primary" runat="server" 
                                                         Text="Card" onclick="btnpatientcard_Click"    ></asp:Button>

                                    <asp:Label ID="LblMsg" ForeColor="Green" Font-Bold=true runat="server" Text=""></asp:Label>

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

