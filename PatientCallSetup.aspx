<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="PatientCallSetup.aspx.cs" Inherits="PatientCallSetup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>

                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Patient call</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Patient call</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    

                    <div  class="regular">
       <div  style="text-align: center " runat="server" id="isc2" visible="false" >
        

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

    <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                      
                                        <div class="input-group date"  data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                        <div class="input-group date"  data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">                                      
                                     <asp:TextBox ID="Txt_Patientname" TabIndex="1" runat="server" placeholder="Enter RegNo" class="form-control" 
                                            AutoPostBack="false"></asp:TextBox>
                                       
                                    </div>
                                </div>
        </div>
                        <div class="row mb-5">
                                <div class="col-sm-6" style="text-align:center" >
<asp:RadioButtonList ID="RblCallStatus" runat="server" CssClass="form-check" RepeatDirection="Horizontal">
    <asp:ListItem>Call Done</asp:ListItem>
    
     <asp:ListItem >Viber</asp:ListItem>
      <asp:ListItem >Whatsapp</asp:ListItem>
      <asp:ListItem Selected="True">Not Done</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            <div class="col-sm-2">
                                  <asp:Button ID="btnclick" runat="server"  class="btn btn-primary" 
                    Text="Click" TabIndex="7" onclick="btnclick_Click" />
                                </div>
                                </div>
                                
                                </div>
                                 
      <div class="box">
                        <div class="box-body">
                           <div class="table-responsive" style="width:100%">
                            <asp:gridview id="GVTestentry" runat="server" class="table table-responsive table-sm table-bordered" datakeynames="PatRegID" autogeneratecolumns="False"
                                width="100%" onrowdatabound="GVTestentry_RowDataBound" allowpaging="True" onpageindexchanging="GVTestentry_PageIndexChanging"
                                pagesize="50" 
        HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"  
 onrowediting="GVTestentry_RowEditing" onselectedindexchanged="GVTestentry_SelectedIndexChanged" OnRowDeleting="GVTestentry_RowDeleting">
                               <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                                 <columns>
                               
                             
<asp:BoundField DataField="Patregdate" HeaderText=" Date" />
<asp:BoundField DataField="PatRegID" HeaderText="Reg.No" />
<asp:BoundField DataField="CenterCode" HeaderText="Center"  />

<asp:BoundField DataField="FullName" HeaderText=" Name" />
<asp:BoundField DataField="Sex" HeaderText="Gender" />
<asp:BoundField DataField="MDY" HeaderText="Age" />
<asp:BoundField DataField="Patphoneno" HeaderText="Phone No" />
<asp:BoundField DataField="EmailID" HeaderText="Email" />
<asp:BoundField DataField="Drname" HeaderText="Ref Doc" />



<asp:TemplateField HeaderText ="Test ">
    <ItemTemplate>
        <asp:Label ID="lbltestname" runat="server" Text='<%#Eval("TestName") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>


<asp:BoundField DataField="SocialMedia" HeaderText="Status" />



    <asp:CommandField EditText="Call Patient" HeaderText="Action"  ShowEditButton="True" />
   <asp:TemplateField HeaderText ="Call remark ">
    <ItemTemplate>
        <asp:Label ID="lblcallremak" runat="server" Text='<%#Eval("CallRemark") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>
   <asp:CommandField DeleteText="Gen Url" Visible="false" HeaderText="Action"  ShowDeleteButton="True" />
   <asp:TemplateField>
    <ItemTemplate>
     <asp:HiddenField ID="hdnFid" runat="server" Value='<%#Eval("FID") %>' /> 
      <asp:HiddenField ID="hdnRegNo" runat="server" Value='<%#Eval("PatRegID") %>' /> 
    </ItemTemplate>
    </asp:TemplateField>


</columns>


                            </asp:gridview>
                             <div class="Pager"></div>
                          </div>
                        </div>
                    </div>

        <asp:Label ID="Label44" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red"
            Height="23px"   Text="Label" Width="525px" Visible="False"></asp:Label><br />
      
     
        <asp:TreeView ID="tvGroupTree" runat="server" ShowCheckBoxes="Leaf" >
            <ParentNodeStyle Font-Bold="False" />
            <RootNodeStyle BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" />
        </asp:TreeView>
        <asp:HiddenField ID="hdReportno" runat="server" />
         <div class="box-footer">
                            <div class="row"  runat="server" id="isc" visible="false">
                             <div class="col-sm-4 "  style="text-align:center" >
                                    <div class="form-group">
                                        
                                     <asp:CheckBox ID="chkiscall"  runat="server" Text="Is Call"></asp:CheckBox>
                                        
                                       
                                    </div>
                                </div>
                                 <div class="col-sm-8" runat="server" id="isc1" visible="false">
                                    <div class="form-group">
                                        
                                     
                                         <asp:TextBox ID="txtemark"  TextMode="MultiLine" runat="server" placeholder="Enter Remark" class="form-control" 
                                            AutoPostBack="false"></asp:TextBox>
                                       
                                    </div>
                                </div>
                                <div class="col-lg-12 text-center">
                                
                                <asp:Button ID="Button2"  runat="server" OnClick="Button2_Click" Text="Action" class="btn btn-primary" />
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

             <cr:crystalreportviewer ID="CVDesc" runat="server" AutoDataBind="true" 
                                ReportSourceID="nnnn"  
                                HasToggleGroupTreeButton="False" Height="9051px" Width="763px" 
                                EnableTheming="True" ShowAllPageIds="True" OnInit="CVDesc_Init" 
                                ReuseParameterValuesOnRefresh="True" SeparatePages="False" 
                                DisplayToolbar="False" OnPreRender="CVDesc_PreRender" PrintMode="ActiveX" />
            <cr:crystalreportsource ID="nnnn" runat="server">
                <Report FileName="~//DiagnosticReport//Pateintreportdescriptive_Email.rpt">
                </Report>
            </cr:crystalreportsource>
              <asp:Label ID="Label6" runat="server" SkinID="errmsg" Text="Default Printer not found !!!"
                    Visible="False" Width="239px"></asp:Label>
                    <asp:HiddenField ID="HiddenField1"   runat="server" />
        <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                                </div>
                                </div>
                                </div>

        </div>


                </section>
                <!-- /.content -->
            
</asp:Content>

