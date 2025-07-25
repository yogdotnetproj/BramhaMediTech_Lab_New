 <%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Hospital.master" CodeFile="useradd.aspx.cs" Inherits="useradd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
       
                <!-- Content Header (Page header) -->
   <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Create User</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Create User</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                            <label>User Category</label>
                                            <div class="radio1">
                                                <label>
                                                    <div class="row mb-3">
                                                        <div class="col-sm-3">
                                                            <label>
                                                                <asp:RadioButtonList ID="RblDType" Width="770Px" runat="server" 
                        RepeatDirection="Horizontal" AutoPostBack="True" CssClass="form-check"
                        onselectedindexchanged="RblDType_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value="0">User</asp:ListItem>
                        <asp:ListItem Value="1">Doctor</asp:ListItem>
                    </asp:RadioButtonList>
                                                            </label>
                                                        </div>    
                                                       
                                                    </div>
                                                </label>
                                            </div>
                                        </div>
                                </div>
                                </div>
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Employee Name <span class="red">*</span></label>
                                        
                                        <asp:TextBox id="ddlempname" runat="server" Visible="false" CssClass="form-control" AutoPostBack="True" 
                            ontextchanged="ddlempname_TextChanged" ></asp:TextBox>
                            <asp:TextBox id="ddlempname1" runat="server"  CssClass="form-control"  ></asp:TextBox>
                        <div style="display: none; overflow: scroll; width: 112px; height: 100px; text-align: right"
                                   id="Div09">
                               </div>
                               <cc1:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
                                   CompletionListElementID="Div09" ServiceMethod="FillInfo" TargetControlID="ddlempname"
                                   MinimumPrefixLength="1">
                               </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>User Name <span class="red">*</span> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton id="lnkavail" onclick="lnkavail_Click" runat="server" Width="122px" SkinID="not" Font-Size="9pt" Font-Names="Verdana">Check Availability</asp:LinkButton></label>
                                      
                                        <asp:TextBox id="txtuname" runat="server" class="form-control" ValidationGroup="vd"></asp:TextBox>

 <asp:Label id="lbluser" runat="server" Width="357px"  Text="User Already Exists.. Enter Another User Name.." Font-Size="8pt" Font-Names="Verdana" Visible="False" ForeColor="Red"></asp:Label> 

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Password <span class="red">*</span></label>
                                      
                                        <asp:TextBox ID="txtpwd" runat="server" ValidationGroup="vd"   class="form-control"></asp:TextBox>
                                    </div>
                                </div>   
                                                             
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Email <span class="red">*</span></label>
                                       
                                        <asp:TextBox id="txtemail" runat="server" class="form-control" ValidationGroup="vd"></asp:TextBox>



                                    </div>
                                </div>
                                </div>
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Phone <span class="red">*</span></label>
                                       
                                        <asp:TextBox id="txtphone" runat="server" MaxLength="13" class="form-control"></asp:TextBox> 

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Mobile <span class="red">*</span></label>
                                      
                                        <asp:TextBox id="txtmobile"  MaxLength="10" runat="server" class="form-control"></asp:TextBox>

                                    </div>
                                </div>

                               
                                

                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Department <span class="red">*</span></label>
                                        
                                        <asp:DropDownList id="ddldept"  runat="server" CssClass="form-control form-select" OnSelectedIndexChanged="ddldept_SelectedIndexChanged" AutoPostBack="True" DataTextField="DeptName"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Lab Name <span class="red">*</span></label>
                                       
                                         <asp:DropDownList ID="ddlLab" runat="server" AppendDataBoundItems="true"
                        DataTextField="DoctorName" DataValueField="DocCode" ForeColor="Navy" 
                        CssClass="form-control form-select">
                        
                    </asp:DropDownList>
                                    </div>
                                </div>
                                </div>
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Set User Type Role <span class="red">*</span></label>
                                       
                                        <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control form-select"  
                        onselectedindexchanged="ddltype_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                                    </div>
                                </div>



                                 <div class="col-sm-3" id="Collid" runat="server"   >
                                    <div class="form-group">
                                        <label>Center Code <span class="red">*</span></label>
                                        <asp:DropDownList id="ddlCentercode" runat="server" CssClass="form-control form-select"
></asp:DropDownList>
                                                                           </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <label>Assign Discount Rights </label>
                                       <asp:CheckBox runat="server" ID="ChkFronddisk" Width="125px" Text="Front Disk" CssClass="form-check" /> 
                                        
                                        .
                                    </div>
                                </div>
                                 <div class="col-sm-2">
                                    <div class="form-group">
                                        <label> </label>
                                      <asp:CheckBox runat="server" ID="ChkBillDesk" Text="Bill Desk" CssClass= "form-check" />
                                        .
                                    </div>
                                </div>
                                <div class="col-sm-4" style="display:none">
                                    <div class="form-group">
                                        <label>ddddddddd </label>
                                        <asp:TextBox id="TextBox2" runat="server" Visible="false" CssClass="form-control"></asp:TextBox> 
                                        .
                                    </div>
                                </div>
                                

                                 <div class="col-lg-12" id="DocDeg" runat="server" visible="false"  >
                                    <div class="form-group">
                                    <table width="50%">
                                    <tr>
                                    <td>
                                    Doctor Degree
                                    </td>
                                    <td >
                                    Upoad Signature
                                    </td>
                                    </tr>
                                     <tr>
                                    <td>
                                    <asp:TextBox id="txtDoctorDegree" runat="server"  class="form-control"></asp:TextBox>
                                    </td>
                                    <td>
                                     <asp:FileUpload ID="FUFileUpload" runat="server"></asp:FileUpload>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td colspan="2" runat="server">
                                    <asp:Image ID="Image1" runat="server"></asp:Image>
                                    
                                    </td>
                                    </tr>
                                    </table>
                                    </div>
                                    </div>

                                       
                               

                            </div>
                        </div>
                        <div class="row">
                             <div class="col-sm-12 text-center">
                                              
                                     <asp:Button ID="Button1" runat="server" OnClick="CmdSave_Click" Text="Save" OnClientClick="return Validate();"  ValidationGroup="vd" CssClass="btn btn-success" />
                              
                                     <asp:Button ID="Button2" runat="server" CausesValidation="False" OnClick="Button2_Click"
                            Text="Cancel"   UseSubmitBehavior="False" CssClass="btn btn-danger" />
                            <asp:Label ID="LBLMsg" runat="server" ForeColor="Red" Font-Bold="true" Text=""></asp:Label>
                             <asp:Button ID="btnreport" runat="server" CausesValidation="False" 
                            Text="Report"  CssClass="btn btn-primary" onclick="btnreport_Click" />
                                        </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                 
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
     </asp:Content>
