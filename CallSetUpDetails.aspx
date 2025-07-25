 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="CallSetUpDetails.aspx.cs" Inherits="PatientCallSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<%--     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>--%>
       <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>

            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Patient call Details </h1>
                    <ol class="breadcrumb">
                  
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Patient call Details</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    

                    <div  class="regular">
       <div  style="text-align: center " runat="server"  >
        

           <div class="col-sm-2"><span class="btn btn-secondary"><strong>Reg # : <asp:Label ID="lblRegNo" runat="server" Text="RegNo"  Font-Bold="True" Width="70px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Name :  <asp:Label ID="lblname" runat="server" Text="Name"  Font-Bold="True" Width="186px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Age :  <asp:Label ID="lblage" runat="server" Text="Age"  Font-Bold="True" Width="51px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Gender :  <asp:Label ID="lblsex" runat="server" Text="Sex"  Font-Bold="True" Width="70px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-12">&nbsp;</div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Test Charges : <asp:Label ID="lbltestcharges" runat="server" Width="96px" 
                                Font-Bold="True"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Center :  <asp:Label ID="lblpscname" runat="server" Width="175px" 
                                Font-Bold="True"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Date : <asp:Label ID="lbldate" runat="server"  Font-Bold="True" Width="142px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Ref Dr. :<asp:Label ID="LblRefDoc" Font-Bold="True" runat="server"  Width="180px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
            <asp:Label ID="Label12" runat="server" Text=" " Width="51px" Font-Bold="True"></asp:Label>
                              
                              <div class="col-lg-12">
                              &nbsp;
                              </div>
     
   
    </div>

    <div class="row">
                                

        <asp:Label ID="Label44" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red"
            Height="23px"   Text="Label" Width="525px" Visible="False"></asp:Label><br />
      
     
        <asp:TreeView ID="tvGroupTree" runat="server" ShowCheckBoxes="Leaf" >
            <ParentNodeStyle Font-Bold="False" />
            <RootNodeStyle BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" />
        </asp:TreeView>
        <asp:HiddenField ID="hdReportno" runat="server" />
         <div class="box-footer">
                            <div class="row"  runat="server" >
                             <div class="col-lg-4 "  style="text-align:center" >
                                    <div class="form-group">
                                        
                                     <asp:CheckBox ID="chkiscall"  runat="server" Text="Is Call" AutoPostBack="True" 
                                            oncheckedchanged="chkiscall_CheckedChanged"></asp:CheckBox>
                                        
                                       
                                    </div>
                                </div>
                                 <div class="col-lg-8" runat="server" >
                                    <div class="form-group">
                                        
                                     
                                         <asp:TextBox ID="txtemark"  TextMode="MultiLine" runat="server" placeholder="Enter Remark" class="form-control" 
                                            AutoPostBack="false"></asp:TextBox>
                                       
                                    </div>
                                </div>
                                <div class="col-lg-12 text-center">
                                 <asp:Button ID="btnprintBalance"  runat="server" Text="Report" class="btn btn-primary" OnClick="btnprintBalance_Click" />
                                <asp:Button ID="Button2"  runat="server" OnClick="Button2_Click" Text="Report" class="btn btn-primary" />
                                  <asp:Button ID="btnaction"  runat="server" Text="Action" class="btn btn-primary" 
                                        onclick="btnaction_Click"  />
                                 <asp:CheckBox ID="Chkemailtopatient" Text="Patient Email"  Width="100px" runat="server"  AutoPostBack="True" 
                                      oncheckedchanged="Chkemailtopatient_CheckedChanged" />
                                        <asp:TextBox ID="txtpatientmail" Text=""  Width="222px" runat="server" 
                                        AutoPostBack="True" ontextchanged="txtpatientmail_TextChanged"    />
                                </div>
                                </div>
                                </div>
          <div id="Div1" class="box-footer" runat="server" visible="false">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   <cr:crystalreportviewer ID="CVTest" runat="server" AutoDataBind="true" 
                                ReportSourceID="mmmm"  
                                HasToggleGroupTreeButton="False" Height="9051px" Width="763px" 
                                EnableTheming="True" ShowAllPageIds="True" OnInit="CVTest_Init" 
                                ReuseParameterValuesOnRefresh="True" SeparatePages="False" 
                                DisplayToolbar="False" OnPreRender="CVTest_PreRender" PrintMode="ActiveX" />
            <cr:crystalreportsource ID="mmmm" runat="server">
                <Report FileName="~//DiagnosticReport//Pateintreportnondescriptive_email.rpt">
                </Report>
            </cr:crystalreportsource>

             <cr:crystalreportviewer ID="CVMemo" runat="server" AutoDataBind="true" 
                                ReportSourceID="nnnn"  
                                HasToggleGroupTreeButton="False" Height="9051px" Width="763px" 
                                EnableTheming="True" ShowAllPageIds="True" OnInit="CVMemo_Init" 
                                ReuseParameterValuesOnRefresh="True" SeparatePages="False" 
                                DisplayToolbar="False" OnPreRender="CVMemo_PreRender" PrintMode="ActiveX" />
            <cr:crystalreportsource ID="nnnn" runat="server">
                <Report FileName="~//DiagnosticReport//Pateintreportdescriptive_Email.rpt">
                </Report>
            </cr:crystalreportsource>
              <asp:Label ID="Label6" runat="server" SkinID="errmsg" Text="Default Printer not found !!!"
                    Visible="False" Width="239px"></asp:Label>
                    <asp:HiddenField ID="HiddenField1"   runat="server" />
        <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
      <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnprintBalance"
    CancelControlID="btnClose" BackgroundCssClass="modalBackground" >
</cc1:ModalPopupExtender>
<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none" BackColor="DimGray">
    <div style="height: 60px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
               &nbsp;
                <asp:Label ID="Label2" runat="server" Text="Bill is pending u want continue?" Font-Bold="True" ForeColor="Red" ></asp:Label>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                    <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:Button ID="btnClose" runat="server" Text="Close" />
</asp:Panel>
                                </div>
                                </div>
                                </div>

        </div>


                </section>
                <!-- /.content -->
            
            <!-- /.content-wrapper -->
            
   
   </asp:Content>