
<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="TestDescriptiveResult.aspx.cs" Inherits="TestDescriptiveResult" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register TagPrefix="cc" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript">
        CKEDITOR.replace('Editor1', {
            height: 10000,
            // Remove the WebSpellChecker plugin.
            removePlugins: 'wsc',
            // Configure SCAYT to load on editor startup.
            scayt_autoStartup: true,
            // Limit the number of suggestions available in the context menu.
            scayt_maxSuggestions: 3
        });
	</script>
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
          <script src="ckeditor/ckeditor.js"></script>
            <!-- Left side column. contains the logo and sidebar -->
              <!-- Content Header (Page header) -->
                   <section class="d-flex justify-content-between content-header mb-2">
                    <h1>Descriptive Result</h1>
                    <ol class="breadcrumb">
                       
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Descriptive Result</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                           <div class="row">
                                 <div class="row mb-2">
                            <div class="col-sm-3"><span ><strong>Reg # : <asp:Label ID="lblRegNo" runat="server" Text="RegNo"  Font-Bold="True" Width="70px"></asp:Label></strong></span></div>
                              
                                <div class="col-sm-3"><span ><strong>Name:  <asp:Label ID="lblName" runat="server" Text="Name"  Font-Bold="True" ></asp:Label></strong></span></div>
                              
                                <div class="col-sm-4"><span ><strong>Age: <asp:Label ID="lblage" ForeColor="Red" runat="server" Text="Age"  ></asp:Label></strong></span></div>
                               
                                <div class="col-sm-2"><span ><strong>Gender :  <asp:Label ID="lblSex" runat="server" Text="Sex"  Font-Bold="True" Width="70px"></asp:Label></strong></span></div>
                              
                                     </div>
                                 <div class="row mb-2">
                                <div class="col-sm-3"><span ><strong>Mobile : <asp:Label ID="LblMobileno" runat="server" 
                                Font-Bold="True"></asp:Label></strong></span></div>
                             
                              <div class="col-sm-3"><span ><strong>Ref Dr. :<asp:Label ID="LblRefDoc" Font-Bold="True" runat="server"  ></asp:Label></strong></span></div>
                               
                                <div class="col-sm-4"><span ><strong>Date : <asp:Label ID="lbldate" runat="server"  Font-Bold="True" ></asp:Label></strong></span></div>
                               
                                
                                 <div class="col-sm-2"><span><strong>Center :  <asp:Label ID="Lblcenter" runat="server"
                                Font-Bold="True"></asp:Label></strong></span></div>
                                     </div>
            <asp:Label ID="Label1" runat="server" Text=" " Width="51px" Font-Bold="True"></asp:Label>
                               <div class="col-lg-12 mt-3">
                            <strong>Clinical History:</strong>   <asp:Label ID="Label6" runat="server" ForeColor="Red" Text=" "  Font-Bold="True"></asp:Label>
                              </div>
                              <div class="col-lg-12">
                              &nbsp;
                              </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        
                                      
                                       
                     <asp:textbox id="txtTestcode" placeholder="Enter Format Name"  runat="server" class="form-control">  </asp:textbox>
                    <asp:requiredfieldvalidator id="TestCodeRequiredFieldValidator" runat="server" controltovalidate="txtTestcode"
                        errormessage="Please enter the subdeptName" style="position: relative" validationgroup="Format"
                        font-bold="True">
                    </asp:requiredfieldvalidator>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                        
                                <asp:DropDownList ID="ddlDoctor_new" runat="server" class="form-control" >
                                                <asp:ListItem Value="0">Select Doctor</asp:ListItem>
                                            </asp:DropDownList>
                                    </div>
                                </div> 
                                <div class="col-lg-4">
                                    <div class="form-group">
                                      
                                       
                                          <asp:dropdownlist id="CmbFormatName" runat="server"  class="form-control" autopostback="True"
                                            onselectedindexchanged="CmbFormatName_SelectedIndexChanged"> </asp:dropdownlist>
                                           
                                            <asp:TextBox ID="txtFormatName"  runat="server"  class="form-control" 
                                        AutoPostBack="true" ontextchanged="txtFormatName_TextChanged"></asp:TextBox>
                                   
                                 
                                    </div>
                                </div>    
                                
                                 <div class="col-lg-12">
                                    <div class="form-group">
                                    <table width="100%" runat="server">
                                    <tr>
                                    <td align="center">
                                    
                                     <label>Authorize</label>
                                    <asp:checkbox id="chkAuthorize" runat="server" Width="166px" />
                                    <asp:label id="lblSelectDocError" runat="server" ForeColor="Green" Font-Bold="true" ></asp:label>
                                    <asp:label style="position: relative" id="lblValidate" runat="server" width="472px"
                        visible="False" skinid="errmsg"></asp:label>
                                    </td>
                                    
                                    </tr>
                                    </table>
                                   
                                    </div>
                                 </div>
                                           
                                            <div class="col-lg-12">
                                    <div class="form-group2">
                                     <table id="Table1" width="94%"  runat="server">
                                    <tr>
                                    <td align="center" >
                                    <label>Result</label>
                               

                                       <asp:TextBox ID="Editor1" runat="server"  TextMode="MultiLine"></asp:TextBox>
<script type="text/javascript" lang="javascript">  CKEDITOR.replace('<%=Editor1.ClientID%>');</script> 
                                      </td>
                                      </tr>
                                      </table>
                                    </div>
                                 </div> 
                                
                                 <div class="col-lg-12">
                                   
                                    <div class="form-group">
                                     <table id="Table3" width="50%" runat="server">
                                    <tr>
                                   
                                    <td align="center" >
                                    <label>Image1</label>                                

                                       
                                      </td>
                                      <td>
                                         
                                      <asp:FileUpload ID="FileUpload1" runat="server" OnDataBinding="FileUpload1_DataBinding"></asp:FileUpload>
                                              
                                      </td>
                                      <td>
                                      
                                      <asp:button ID="Imgb12" runat="server" Visible="false" Text="Upload1"  onclick="Imgb1_Click"></asp:button>
                                      
                                      </td>
                                      <td runat="server"  >
                                     
                                <asp:Image ID="Image1" runat="server" Visible="false"  Height = "100" Width = "100" />
                                    <asp:Label ID="Image6" runat="server" Text="" Height = "70" Width = "100" />           
                                    
                                      </td>
                                     
                                      </tr>
                                       <tr>
                                      
                                    <td align="center" >
                                    <label>Image2</label>                               

                                       
                                      </td>
                                      <td>
                                      <asp:FileUpload ID="FileUpload2" runat="server"></asp:FileUpload>
                                      </td>
                                       <td>
                                        
                                      <asp:button ID="ImageButton1" 
                                              runat="server" Text="Upload2" Visible="false" onclick="ImageButton1_Click" ></asp:button>
                                              
                                      </td>
                                       <td>
<asp:Image ID="Image2" runat="server" Visible="false" Height = "100"  Width = "100" />
                                            <asp:Label ID="Label2" runat="server" Text="" Height = "70" Width = "100" /> 
                                      
                                      </td>
                                      
                                      </tr>
                                       <tr>
                                    <td align="center" >
                                    <label>Image3</label>
                                                                        
                                      </td>
                                      <td>
                                      <asp:FileUpload ID="FileUpload3" runat="server"></asp:FileUpload>
                                      </td>
                                       <td>
                                      <asp:button ID="ImageButton2" 
                                              runat="server" Text="Upload3" Visible="false" onclick="ImageButton2_Click" ></asp:button>
                                      </td>
                                       <td>
<asp:Image ID="Image3" runat="server" Height = "100" Visible="false" Width = "100" />
                                            <asp:Label ID="Label3" runat="server" Text="" Height = "70" Width = "100" /> 
                                      
                                      </td>
                                      </tr>
                                       <tr>
                                    <td align="center" >
                                    <label>Image4</label>
                                  
                                      </td>
                                      <td>
                                      <asp:FileUpload ID="FileUpload4" runat="server"></asp:FileUpload>
                                      </td>
                                       <td>
                                      <asp:button ID="ImageButton3" 
                                              runat="server" Text="Uplaod4" Visible="false" onclick="ImageButton3_Click" ></asp:button>
                                      </td>
                                       <td>
<asp:Image ID="Image4" runat="server" Height = "100" Visible="false" Width = "100" />
                                            <asp:Label ID="Label4" runat="server" Text="" Height = "70" Width = "100" /> 
                                      
                                      </td>
                                      </tr>
                                       <tr>
                                    <td align="center" >
                                    <label>Image5</label>
                                  
                                      </td>
                                      <td>
                                      <asp:FileUpload ID="FileUpload5" runat="server"></asp:FileUpload>
                                      </td>
                                       <td>
                                      <asp:button ID="ImageButton4" 
                                              runat="server" Text="Upload5"  Visible="false" onclick="ImageButton4_Click" ></asp:button>
                                      </td>
                                       <td>
<asp:Image ID="Image5" runat="server" Height = "100" Visible="false" Width = "100" />
                                            <asp:Label ID="Label5" runat="server" Text="" Height = "70" Width = "100" /> 
                                      
                                      </td>
                                      </tr>
                                      </table>
                                    </div>
                                 
                                 </div> 
                                    
                           </div>
                        </div>
                       
                    </div>
                  
                    <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">   
                                   <asp:button id="btnsave" runat="server" class="btn btn-success" text="Save" onclick="cmdSaveClose_Click"   />
                                    <asp:button id="cmdClose" runat="server" text="Close"  class="btn btn-primary" causesvalidation="False" onclick="cmdClose_Click"  />
                                    <asp:button id="cmdClear" runat="server" text="Save Format" class="btn btn-warning" onclick="cmdClear_Click"
                                        validationgroup="Format" />
                                           <asp:button id="Button1" runat="server" text="Save & Print" 
                                         class="btn btn-success"     
                                        onclick="Button1_Click" />

                                    <asp:button id="btnPRints" runat="server" text="Print" 
                                         class="btn btn-success" OnClick="btnPRints_Click"     />
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
               <asp:sqldatasource id="sqlFormat" runat="server" connectionstring="<%$ ConnectionStrings:myconnection %>"
        selectcommand="select [Name], Result,STCODE from dfrmst where STCODE=@STCODE and branchid=@branchid order by Name">
        <selectparameters>
            <asp:QueryStringParameter Name="STCODE" QueryStringField="STCODE" />
            <asp:SessionParameter DefaultValue="0" Name="branchid" SessionField="branchid" />
        </selectparameters>
    </asp:sqldatasource>
    <asp:hiddenfield id="hdnSort" runat="server" />
     <script>
         CKEDITOR.replace('Editor1', {
             height: 13000,
             // Remove the WebSpellChecker plugin.
             removePlugins: 'wsc',
             // Configure SCAYT to load on editor startup.
             scayt_autoStartup: true,
             // Limit the number of suggestions available in the context menu.
             scayt_maxSuggestions: 3
         });
	</script>
   </asp:Content>
