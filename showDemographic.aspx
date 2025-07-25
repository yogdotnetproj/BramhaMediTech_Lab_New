 <%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Hospital.master" CodeFile="showDemographic.aspx.cs" Inherits="showDemographic" %>  

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>  
     
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Patient Clinical History</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Patient Clinical History</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <%-- <asp:UpdatePanel ID="UpdatePaneLeftBox" runat="server" style="">

            <ContentTemplate>--%>
                <div class="row">
                  <div class="col-md-12">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-sm-3">
                                               
                                                 <asp:DropDownList ID="cmbInitial"  runat="server" class="form-control form-select" OnSelectedIndexChanged="cmbInitial_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-9">
                                               
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
                                <div class="col-md-3">
                                    <div class="form-group">
                                      
                                        <div class="row">
                                            <div class="col-sm-12">
                                                
                                                
<asp:TextBox id="txtsex"  runat="server" placeholder="Enter Gender" class="form-control"></asp:TextBox> 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-sm-6">
                                              
                                                <asp:TextBox id="txtAge" runat="server" MaxLength="3" placeholder="Age" class="form-control"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAge"
        Display="Dynamic" ErrorMessage="This field is required" SetFocusOnError="True" ValidationGroup="ValidateForm" Font-Bold="True"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtAge"
        ErrorMessage="Please Enter Valid Age" Operator="DataTypeCheck" Style="position: relative"
        Type="Integer" ValidationGroup="ValidateForm" SetFocusOnError="True" Display="Dynamic" Font-Bold="True"></asp:CompareValidator>
                                            </div>
                                            <div class="col-sm-6">
                                                
                                                 <asp:DropDownList id="cmdYMD" runat="server" class="form-control form-select">
                <asp:ListItem>Year</asp:ListItem>
                <asp:ListItem>Month</asp:ListItem>
                <asp:ListItem>Day</asp:ListItem>
            </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                 <div class="col-md-2">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                                                                <asp:TextBox ID="txtBirthdate" placeholder="Enter DOB" runat="server" class="form-control" TabIndex="80" MaxLength="15" ></asp:TextBox>
                                                                                <span class="input-group-addon">
                                                                                    <i class="fa fa-calendar"></i>
                                                                                </span>
                                                                            </div>
                                                </div>
                                            </div>
                                        </div>
                                     </div>
                                
                                <div class="col-md-2">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-sm-12">
                                                
                                                <asp:TextBox ID="txttelno" runat="server" class="form-control" placeholder="Enter Mobile" AutoPostBack="True" OnTextChanged="txttelno_TextChanged"></asp:TextBox>
        <asp:Label ID="lbl_mobile" runat="server" Font-Bold="true" ForeColor="Red" Visible="false" SkinID="errmsg" ></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-sm-12">
                                                
                                                  <asp:TextBox ID="txtemail" runat="server" class="form-control"  placeholder="Enter Email" AutoPostBack="True" OnTextChanged="txtemail_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-sm-12">
                                               
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
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-4">
                                    <div class="form-group">
                                     
                                       
                                        <asp:TextBox id="txt_address" runat="server" class="form-control"  placeholder="Enter Address" TextMode="MultiLine" TabIndex="10"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-4" runat="server" visible="false">
                                    <div class="form-group">
                                      
                                        
                                        <asp:TextBox id="txt_clinicalhistory" runat="server" class="form-control"  placeholder="Enter Patient Clinical History" TextMode="MultiLine" ></asp:TextBox>
 

                                    </div>
                                </div>
                                <div class="col-md-8">
                                    <div class="form-group">
                                     
                                        
                                          <asp:TextBox id="txt_remark" runat="server"   TextMode="MultiLine" class="form-control"  placeholder="Enter Remark"></asp:TextBox>

                                    </div>
                                </div>

                            </div>
                            <div class="row mb-3">
                                  <!-------------------------------------------------------------->

                                             <div class="col-sm-3"  runat="server" id="D1" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-sm-12">
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
                                            <div class="col-sm-3" runat="server" id="D2" visible="false">
                                                <div class="form-group">
                                                    
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txtHeight" placeholder="Enter Height" runat="server" Height="30px"  class="form-control">     </asp:TextBox>
                                                      </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6" runat="server" id="D3" visible="false">
                                                <div class="form-group">
                                                  
                                                    <div class="row">
                                                        <div class="col-sm-12">
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


                                              <div class="col-sm-3" runat="server" id="D4" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-sm-12">
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
                                            <div class="col-sm-3" runat="server" id="D5" visible="false">
                                                <div class="form-group">
                                                    
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txtFSTime" placeholder="Enter FSTime" runat="server" Height="30px" class="form-control">     </asp:TextBox>
                                                      </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6" runat="server" id="D6" visible="false">
                                                <div class="form-group">
                                                  
                                                    <div class="row">
                                                        <div class="col-sm-12">
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

                                              <div class="col-sm-12" runat="server" id="D7" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-sm-12">
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

                                            <div class="col-sm-12" runat="server" id="D8" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                             <asp:RadioButtonList ID="rblretecate"  Height="40px" CssClass="form-control form-check" runat="server"  
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



                                <div class="col-sm-6">
                                    <div class="form-group">
                                       
                                       
                                        <asp:Label id="lblExist" runat="server" SkinID="errmsg" ></asp:Label>
<asp:TextBox id="txttests" runat="server" class="form-control"  placeholder="Select Test / Package" AutoPostBack="True" OnTextChanged="txttests_TextChanged"></asp:TextBox>
 <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 348px; HEIGHT: 120px" id="divtest"></DIV>
 <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="divtest" ServiceMethod="FillTests" TargetControlID="txttests" MinimumPrefixLength="1"></cc1:AutoCompleteExtender> 

                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <label>Select Test / Package <span class="red">*</span></label>
                                       
                                         Total Amount =
        <asp:Label ID="lblTotTestAmt" runat="server" multiple class="form-control" >0</asp:Label> 
                                         
                                    </div>
                                </div>
                                <div class="col-sm-12" runat="server"  visible="false" >
                                   <div class="table-responsive" style="width:100%">
            <asp:UpdatePanel id="UpdatePanel6" runat="server">
                <contenttemplate>
<asp:GridView ID="GrdPackage" runat="server" class="table table-responsive table-sm table-bordered" AutoGenerateColumns="False" Width="100%" 
  OnRowDataBound="GrdPackage_RowDataBound"   HeaderStyle-ForeColor="Black" >
    <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
<Columns>
<asp:BoundField DataField="SampleType" HeaderText="SampleType" />
<asp:BoundField DataField="STCODE" HeaderText="Test Codes" />
<asp:BoundField DataField="TestName" HeaderText="Test Names" />
<asp:TemplateField HeaderText="Barcode">
    <ItemTemplate>
        <asp:TextBox ID="txtbarcodeid" runat="server"></asp:TextBox>      
        <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtbarcodeid"
                                                              FilterMode="ValidChars" ValidChars="A,B,C,D,E,F,E,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,0,1,2,3,4,5,6,7,8,9">
                                                      </cc1:FilteredTextBoxExtender>
          
         <asp:Label id="lblError" runat="server" SkinID="errmsg"></asp:Label>
         <asp:Label id="lblRequiredField" runat="server" SkinID="errmsg" Visible="false"></asp:Label>
    </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField >
<ItemTemplate>
<asp:TextBox ID="txtair" runat="server" Text="" Width="100px" Visible="false"></asp:TextBox>
</ItemTemplate>
</asp:TemplateField>


<asp:TemplateField HeaderText="Lab Name" Visible="false">
<ItemTemplate>
 <div style="width:200px; overflow:hidden ">
                 <asp:DropDownList ID="ddllab" runat="server" Width="350px" CssClass="form-select">
                </asp:DropDownList>
                 </div>
</ItemTemplate>
    <ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>

</Columns>
    
</asp:GridView>

                </contenttemplate>
            </asp:UpdatePanel>
            </div>
                                </div>
                            </div>
                        </div>
                      





                    </div>
                    <div class="box">
                      <div class="table-responsive" style="width:100%">
                         <asp:GridView ID="grdTests" runat="server" class="table table-responsive table-sm table-bordered" AutoGenerateColumns="False" 
                Width="100%" AutoGenerateDeleteButton="True"
                                CellPadding="4" DataKeyNames="MTCode" 
                OnRowDeleting="grdTests_RowDeleting" OnRowDataBound="grdTests_RowDataBound"  
                  HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White" 
                  PageSize="150">
                               <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                                <Columns>
                                    <asp:BoundField DataField="MTCode" HeaderText="Test/Package Code" />
                                    <asp:BoundField DataField="Maintestname" HeaderText="Test/Package Name" />
                                    <asp:TemplateField HeaderText="Package/Test">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPorT" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Amount" HeaderText="Test Rate" />
                                     <asp:BoundField DataField="ClientAmount" HeaderText="Client Rate" />
                                     <asp:TemplateField HeaderText="Modify Rate" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtModifyRate" BorderColor="Red" Text="0" Font-Bold="true" Width="100px" AutoPostBack="true" class="form-control"  runat="server" OnTextChanged="txtModifyRate_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                               

                                <HeaderStyle ForeColor="Black" />
                               

          </asp:GridView>
                           <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="2000" DynamicLayout ="true">
        <ProgressTemplate>
        <center><img id="Img1" src="~/Images0/Progress-Bar.png" runat="server" /></center>
        </ProgressTemplate>
        </asp:UpdateProgress>
                        </div>
                    </div>
                      <div class="box-footer">
                            <div class="row mb-3">
                                <div class="col-md-12 text-center">
                                     <table>
                                                                    <tr>
                                                                        <td>
                                                                            <%-- <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                                                    <ContentTemplate>--%>
                                                                           <asp:FileUpload ID="FUBrowsePresc" runat="server"></asp:FileUpload>
                                                                            <asp:Label ID="LblFilename" Text="" Font-Bold="true" runat="server"></asp:Label>
                                                                        
                                                                        <%-- </ContentTemplate>
                                                                </asp:UpdatePanel>--%>
                                                                        </td>
                                                                        <td>
                                                                            
                                                                            <asp:Button ID="btnupload" runat="server" Visible="true" Textclass="btn btn-primary" 
                                                                                Text="Upload Signature" OnClick="btnupload_Click" CssClass="btn btn-primary"></asp:Button>
                                                                           
                                                                        </td>
                                                                        <td rowspan="3">
                                                                            <asp:Image ID="Image1show" Width="70px" Height="70px" runat="server"/>
                                                                        </td>
                                                                    </tr>
                                         <tr>
                                             <td colspan="2">
                                                 </td>
                                         </tr>
                                                            <tr>
                                                                
                                                                        <td>
                                                                            <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>--%>
                                                                           <asp:FileUpload ID="FUUploadPhoto" runat="server"></asp:FileUpload>
                                                                            <asp:Label ID="LblUploadph" Text="" Font-Bold="true" runat="server"></asp:Label>
                                                                        
                                                                        <%-- </ContentTemplate>
                                                                </asp:UpdatePanel>--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="btnbrowsphoto" runat="server" Visible="true" Textclass="btn btn-primary" 
                                                                                Text="Upload Photo"  CssClass="btn btn-primary" OnClick="btnbrowsphoto_Click"></asp:Button>
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                    </div>
                                </div>
                          </div>
                     <div class="box-footer">
                            <div class="row">
                                <div class="col-md-12 text-center">
                                  <asp:Button ID="btnBack" runat="server" class="btn btn-secondary" Text="Back" OnClick="btnBack_Click"  />
                                  
                                    <asp:Button ID="btnSubmit" runat="server" class="btn btn-success"  OnClientClick="this.disabled=true;" UseSubmitBehavior="false"
                 OnClick="btnSubmit_Click" Text="Save " ValidationGroup="ValidateForm"      />

                 <asp:Button ID="btnpayment" runat="server" Visible="false" class="btn btn-info" 
                Text="Payment" onclick="btnpayment_Click"      />
                <asp:Button ID="btnbprint" runat="server" Text="Sample Type Wise barcode"  
                                                         class="btn btn-primary" onclick="btnbprint_Click" ></asp:Button>
                                                         <asp:Button ID="btnpatientcard"  class="btn btn-secondary" runat="server" 
                                                         Text="Card" onclick="btnpatientcard_Click"    ></asp:Button>
                                                         <asp:Button ID="btnbarcodeEntry" runat="server" Text="Dept wise barcode"  
                                                         class="btn btn-info" onclick="btnbarcodeEntry_Click"></asp:Button>
                                    <asp:Button ID="btnbilldesk" runat="server" Text="Bill Desk"  
                                                         class="btn btn-primary" OnClick="btnbilldesk_Click" ></asp:Button>

                                     <asp:Button ID="btncapturephoto" runat="server" Text="Capture Photo"  
                                                         class="btn btn-secondary" OnClick="btncapturephoto_Click"  ></asp:Button>
                                     <asp:Button ID="btnPrint" runat="server"  class="btn btn-success" 
                Text="Print" onclick="btnPrint_Click"      />
                                </div>
                            </div>
                          <div class="row">
                                <div class="col-md-12 text-center">
                                     <asp:Label ID="lblText" runat="server" Font-Bold="true" ForeColor="Green" Text=""></asp:Label>
                                    </div>
                              </div>
                        </div>



                    </div>
                  

                     <div class="col-md-3 bg-light" runat="server" visible="false">
                            <div class="box">
                                
                                <div class="box-body">
                                    <div class="form-group">
                                    <div class="rounded_corners text-center" style="width:100%; height: 1000px; vertical-align:top;  overflow:scroll ;  "  >
                            
                           
<asp:CheckBoxList ID="Chkmaintestshort" runat="server" RepeatColumns="2" CssClass="form-check table-bordered"  Width="100%" AutoPostBack="True" 
                                    onselectedindexchanged="Chkmaintestshort_SelectedIndexChanged" ></asp:CheckBoxList>



</div>
                                    </div>
                               </div>
                            </div>
                    </div>
                      </div>
               <%-- </ContentTemplate>
                         </asp:UpdatePanel>--%>
                </section>
                <!-- /.content -->
           
     
        <!-- ./wrapper -->
        <asp:UpdatePanel ID="UpdatePanel32" runat="server">          
        <Triggers>
         
              <asp:PostBackTrigger ControlID="btnSubmit" />
              <asp:PostBackTrigger ControlID="btnpayment" />
             <asp:PostBackTrigger ControlID="btnbprint" />
            <asp:PostBackTrigger ControlID="btnpatientcard" />
              <asp:PostBackTrigger ControlID="btnbarcodeEntry" />
              <asp:PostBackTrigger ControlID="btncapturephoto" />
            
           
            
        </Triggers>
          </asp:UpdatePanel>
         <script language="javascript" type="text/javascript">
             function OpenReport() {
                 window.open("Reports.aspx");
             } 
               </script>

               
       
    </asp:Content>
