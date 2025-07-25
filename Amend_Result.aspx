<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="Amend_Result.aspx.cs" Inherits="Amend_Result" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
     <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <%  <Triggers>
              <asp:PostBackTrigger ControlID="btnreport" />
                <asp:PostBackTrigger ControlID="btnPdf" />
           </Triggers>
        <ContentTemplate>--%>
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Amend Result Entry</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Amend Result Entry</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box" style="margin-bottom:1px">
                        <div class="box-body" style="padding-bottom:0px">
                            <div class="row mb-2">
                                <div class="col-sm-3">
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
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        <div class="input-group date"  data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                        <!--    <input type="text" class="form-control pull-right" id="todate">-->
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                    

                             <asp:textbox id="txtPPID" class="form-control" placeholder="Select PPID No" runat="server" AutoPostBack="false">
                            </asp:textbox>
                             
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       <!-- <input type="text" class="form-control pull-right" placeholder="Enter Registration Number"> -->
                                        <asp:textbox id="txtregno"  runat="server" class="form-control" tabindex="5" placeholder="Enter Registration Number">
                            </asp:textbox>
                                    </div>
                                </div> 
                                
                                 </div>
                            <div class="row mb-2">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <!--  <input type="text" class="form-control pull-right" placeholder="Enter Patient Name">-->
                                        <asp:textbox id="txtPatientName" runat="server" class="form-control" style="position: relative" tabindex="4" placeholder="Enter Patient Name">
                            </asp:textbox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                     <!--   <input type="text" class="form-control pull-right" placeholder="Enter Mobile Number"> -->
                                         <asp:textbox id="txtmobileno" runat="server" class="form-control" placeholder="Enter Ref Doc" tabindex="5">
                            </asp:textbox>
                              <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: left"
                                                        id="Div4">
                                                    </div>
                             <cc1:AutoCompleteExtender id="AutoCompleteExtender3" runat="server" MinimumPrefixLength="1" 
                            TargetControlID="txtmobileno" ServiceMethod="GetDoctor"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="Div4">
                            </cc1:AutoCompleteExtender>
                                    </div>
                                </div> 
                                <div class="col-sm-3">
                                    <div class="form-group"> 
                                            <asp:textbox id="txttestname" placeholder="select Dept Name" class="form-control" runat="server" >
                            </asp:textbox>
                            <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: left"
                                                        id="Div3">
                                                    </div>
                                         <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1" 
                            TargetControlID="txttestname" ServiceMethod="GetDeptName"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="Div3">
                            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       <!-- <input type="text" class="form-control pull-right" placeholder="Enter Registration Number"> -->
                                        <asp:textbox id="txtcentername_new" class="form-control" placeholder="Center Name" runat="server" AutoPostBack="false">
                            </asp:textbox>
                             <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: left"
                                                        id="Div5">
                                                    </div>
                              <cc1:AutoCompleteExtender id="AutoCompleteExtender4" runat="server" MinimumPrefixLength="1" 
                            TargetControlID="txtcentername_new"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ServiceMethod="Getcenter" CompletionListElementID="Div5">
                            </cc1:AutoCompleteExtender> 
                                    </div>
                                </div>  
                                 </div>
                                <div class="row mb-2">
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       <!-- <input type="text" class="form-control pull-right" placeholder="Enter Registration Number"> -->
                                        <asp:textbox id="txtreportDoneBy" class="form-control" placeholder="Remark" runat="server" AutoPostBack="false">
                            </asp:textbox>
                            <%-- <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: right"
                                                        id="Div1">
                                                    </div>
                              <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1" 
                            TargetControlID="txtreportDoneBy" ServiceMethod="Getcenter" CompletionListElementID="Div1">
                            </cc1:AutoCompleteExtender> --%>
                                    </div>
                                </div>   
                                
                                 <div class="col-sm-3">
                                    <div class="form-group"> 
                                     <asp:TextBox ID="txtbarcodeNo" placeholder="Enter Barcode No"  runat="server"  class="form-control" TabIndex="5">
                </asp:TextBox>
                             
                                    </div>
                                    </div>
                                <div class="col-sm-3">
                                    <div class="form-group">    
                                    <asp:Label ID="Label1" runat="server" Font-Bold="true"  Text="Total Patient Count is: "></asp:Label>
<asp:Label ID="lblpcount" runat="server" Font-Bold="true" ForeColor="black" Text=""></asp:Label>
                                    </div>
                                    </div>  

                                     <div class="col-sm-3">
                                    <div class="form-group"> 
                                    <asp:button id="btnList" runat="server" onclick="btnList_Click" class="btn btn-primary" text="Click"
                                 onclientclick=" return validate();" tabindex="7" />
                                    </div>
                                    </div>  
                                     </div>
                                    <div class="row mb-3">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        
                                <%--        <asp:RadioButtonList ID="ddlStatus" runat="server" placeholder="select status" RepeatDirection="Horizontal" 
                                            CssClass="form-check" AutoPostBack="True" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
    <asp:ListItem  >Pending</asp:ListItem>
    <asp:ListItem >Completed</asp:ListItem>
     <asp:ListItem>Tested</asp:ListItem>
      <asp:ListItem>Authorized</asp:ListItem>
       <asp:ListItem>Emergency</asp:ListItem>
        <asp:ListItem>IntRece</asp:ListItem>
         <asp:ListItem>IntNotRece</asp:ListItem>
         <asp:ListItem>Outsource</asp:ListItem>
          <asp:ListItem>Abnormal</asp:ListItem>
    <asp:ListItem >All</asp:ListItem>
   
                                        </asp:RadioButtonList>--%>


                                       
                            <asp:sqldatasource id="sqlStatus" runat="server" connectionstring="<%$ ConnectionStrings:myconnection %>"
                                selectcommand="SELECT * FROM [DrMT]  where DrType='CC' order by DoctorName">
                            </asp:sqldatasource>
                                        </div>
                                    </div>
                                        </div>
                            <div class="row mb-3" runat="server" visible="false"> 
                                    
                                    <div class="form-group pl-2 d-flex">
                                        <div class="col">
                                    <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" runat="server"  onclick="ON_Pending()" id="ChkPending"/>
                                      <label class="form-check-label" for="flexSwitchCheckDefault">Pending</label>
                                    </div>
                                        </div>
                                        <div class="col">
                                        <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" id="ChkCompleted"  onclick="ON_Completed()" runat="server" />
                                      <label class="form-check-label" for="flexSwitchCheckDefault">Completed</label>
                                    </div>
                                            </div>
                                        <div class="col">
                                        <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" id="ChkTested"  onclick="ON_Tested()" runat="server"/>
                                      <label class="form-check-label" for="flexSwitchCheckDefault">Tested</label>
                                    </div>
                                            </div>
                                        <div class="col">
                                        <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" id="ChkAuthorizs"  onclick="ON_Authorizs()" runat="server"/>
                                      <label class="form-check-label" for="flexSwitchCheckDefault">Authorized</label>
                                    </div>
                                            </div>
                                        <div class="col">
                                        <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" id="ChkEmergency"  onclick="ON_Emergency()" runat="server"/>
                                      <label class="form-check-label" for="flexSwitchCheckDefault">Emergency</label>
                                    </div>
                                            </div>
                                        <div class="col">
                                        <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" id="ChkIntRece"  onclick="ON_IntRece()" runat="server"/>
                                      <label class="form-check-label" for="flexSwitchCheckDefault">IntRece</label>
                                    </div>
                                            </div>
                                        <div class="col">
                                        <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" id="ChkIntNotReceive"  onclick="ON_IntNotReceive()" runat="server" />
                                      <label class="form-check-label" for="flexSwitchCheckDefault">IntNotRece</label>
                                    </div>
                                            </div>
                                        <div class="col">
                                        <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" id="ChkOutsource"  onclick="ON_Outsource()" runat="server"/>
                                      <label class="form-check-label" for="flexSwitchCheckDefault">Outsource</label>
                                    </div>
                                            </div>
                                        <div class="col">
                                        <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" id="ChkAbnormal"  onclick="ON_Abnormal()" runat="server"/>
                                      <label class="form-check-label" for="flexSwitchCheckDefault">Abnormal</label>
                                    </div>
                                            </div>
                                        <div class="col">
                                        <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" id="ChkAll"  onclick="ON_All()" runat="server" />
                                      <label class="form-check-label" for="flexSwitchCheckDefault">All</label>
                                    </div>
                                            </div>
                                        </div>
                                        
                                         
                                        
                                           
                            </div>                                  
                        </div>
                       
                    </div>
                     <div class="box">
                        <div class="box-body" style="margin-top:-10px">
                           <div class="table-responsive" style="width:100%">
                            <asp:gridview id="GVTestentry" runat="server"   class="table table-responsive table-sm  table-bordered" datakeynames="PatRegID" autogeneratecolumns="False"
                                width="100%" onrowdatabound="GVTestentry_RowDataBound" allowpaging="True" onpageindexchanging="GVTestentry_PageIndexChanging"
                                pagesize="100" 
        HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"  
 onrowediting="GVTestentry_RowEditing" onselectedindexchanged="GVTestentry_SelectedIndexChanged"  OnRowCommand="GVTestentry_RowCommand">
                                <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                                <columns>
                               
                                <asp:HyperLinkField HeaderText="Submit" Text="Submit" Visible="false"  DataNavigateUrlFields="PatRegID,FID,SampleStatusNew,CenterCode" DataNavigateUrlFormatString="Addresult.aspx?PatRegID={0}&amp;FID={1}&amp;CN={3}&amp;TS={2}" />
 <asp:BoundField DataField="DailyseqNo" HeaderText="Seq no" />
                                    <asp:BoundField DataField="RegistratonDateTime" HeaderText=" Date" />
<asp:BoundField DataField="PatRegID" HeaderText="RegNo" />
                                     <asp:BoundField DataField="PPID" HeaderText=" PPID" />
<asp:BoundField DataField="CenterCode" HeaderText="Center"  />


  <asp:TemplateField HeaderText="FullName">
                            <ItemTemplate>
                            <asp:Label ID="lblfullname" runat="server" Text='<%#Eval("FullName") %>'></asp:Label>
                            <asp:ImageButton ID="btnEmergency"  class="flashingTextcss" Visible="false" ImageUrl="~/images/light-311119__340.png" runat="server"></asp:ImageButton>

                            </ItemTemplate>
                            </asp:TemplateField>

<asp:BoundField DataField="Sex" HeaderText="Gender" />
<asp:BoundField DataField="MYD" HeaderText="Age" />
<asp:BoundField DataField="DrName" HeaderText="Ref Dr" />


<asp:HyperLinkField HeaderText="Test" DataTextField="TestName"  DataNavigateUrlFields="PatRegID,FID,MTCode" DataNavigateUrlFormatString="Addresult.aspx?PatRegID={0}&amp;FID={1}&amp;TestCode={2}" />


<asp:BoundField DataField="SampleStatusNew" HeaderText=" Status" />
                                    <asp:TemplateField HeaderText="Card">
    <ItemTemplate>
<asp:ImageButton ID="btnpatientcard" runat="server" ImageUrl="~/Images0/edit.gif"  class="btn btn-primary" 
            CommandArgument='<%#Eval("PID") %>' Text="Patient Card" 
            onclick="btnpatientcard_Click"></asp:ImageButton>
    </ItemTemplate>
    </asp:TemplateField>
 <asp:BoundField DataField="P_remark" HeaderText="Remark" Visible="false" />
<asp:BoundField DataField="LabRegMediPro" HeaderText="MedNo" Visible="false" />
<asp:BoundField DataField="Labno" HeaderText="LNo" Visible="false" />
    <asp:CommandField EditText="Report" HeaderText="Report"  ShowEditButton="True" />
   
   <asp:BoundField DataField="ReRunType" HeaderText="Rerun"  Visible="false"/>
   <asp:BoundField DataField="MailStatus" HeaderText=" PMail" />
   <asp:BoundField DataField="InterfaceStatus" HeaderText=" Interface" />
   
     <asp:BoundField DataField="DrMailStatus" HeaderText=" DrMail" Visible="false" />
                                    <asp:BoundField DataField="Isoutsource" HeaderText="Outsource" Visible="false" />
                                   
    
      <asp:TemplateField HeaderText="Panic">
                            <ItemTemplate>
                            <asp:Label ID="lblpanic" runat="server" Text='<%#Eval("PanicResult") %>'></asp:Label>
                            <asp:ImageButton ID="btnpanic"  class="flashingTextcss" ImageUrl="~/images/light-311119__340.png" runat="server"></asp:ImageButton>

                            </ItemTemplate>
                            </asp:TemplateField>
       <asp:BoundField DataField="SpecimanNo" HeaderText=" SrNo" Visible="false" />
     
       
                                   
                                     <asp:BoundField DataField="Balance" HeaderText=" Balance" Visible="false" />
                                     <asp:TemplateField HeaderText="Upload">
                    <ItemTemplate>
                        <asp:FileUpload ID="FileUpload1" runat="server" EnableViewState="true" />
                        <asp:Label ID="LblFilename" Text="" Font-Bold="true" runat="server"></asp:Label>
                        <asp:HyperLink ID="Hyp_viewPres" runat="server" Visible="false"  NavigateUrl='<%# Eval("UploadPrescription") %>'>View Pres</asp:HyperLink>
                         <asp:ImageButton ID="Imgrerun" runat="server" Width="20px"  TabIndex="9993" ToolTip="View Pres"  CommandArguement='<%#Eval("UploadPrescription")%>' onclick="Imgrerun_Click"  ImageUrl="~/images/ReRun.png"   ></asp:ImageButton>
                        <asp:Button ID="saveBtn" runat="server"
                            CommandArgument="<%# Container.DataItemIndex%>" CommandName="save"
                            Text="OK"/>
                    </ItemTemplate>
                </asp:TemplateField>
<asp:TemplateField>
    <ItemTemplate>
    
        <asp:HiddenField ID="hdnFID1" runat="server" Value='<%#Eval("FID") %>' /> 
        <asp:HiddenField ID="HdnPercent" runat="server" Value='<%#Eval("PendingPer") %>' /> 
        <asp:HiddenField ID="hdnopid" runat="server" Value='<%#Eval("OutsourcePatientPID") %>' /> 
        <asp:HiddenField ID="hdnORT" runat="server" Value='<%#Eval("OutResTransfer") %>' /> 
        <asp:HiddenField ID="hdnis_emergency" runat="server" Value='<%#Eval("Isemergency") %>' />
        <asp:HiddenField ID="HDPID" runat="server" Value='<%#Eval("PID") %>' /> 
         <asp:HiddenField ID="hdnPatRegID" runat="server" Value='<%#Eval("PatRegID") %>' /> 
          <asp:HiddenField ID="hdnMaindept" runat="server" Value='<%#Eval("DigModule") %>' /> 
           <asp:HiddenField ID="hdn_Maintestname" runat="server" Value='<%#Eval("TestName") %>' /> 
           <asp:HiddenField ID="hdn_MTcode" runat="server" Value='<%#Eval("MTCode") %>' /> 
        <asp:HiddenField ID="hdn_PEDate" runat="server" Value='<%#Eval("PEDate") %>' /> 
        <asp:HiddenField ID="HDPPID" runat="server" Value='<%#Eval("PPID") %>' /> 
       <asp:HiddenField ID="hdnUploadPrescription" runat="server" Value='<%#Eval("UploadPrescription") %>' /> 
    </ItemTemplate>
</asp:TemplateField>


</columns>

 <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#38C8DD" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:gridview>
                             <div class="Pager"></div>
                          </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
          
            <script src="plugins/Emergency.js"></script>
        <script>
            $(document).ready(function () {
                var speed = 500;
                function effectFadeIn(classname) {
                    $("." + classname).fadeOut(speed).fadeIn(speed, effectFadeOut(classname))
                }
                function effectFadeOut(classname) {
                    $("." + classname).fadeIn(speed).fadeOut(speed, effectFadeIn(classname))
                }
                //Calling fuction on pageload
                $(document).ready(function () {
                    effectFadeIn('flashingTextcss');
                });
            });
  </script>
      <script language="javascript" type="text/javascript">

          function ON_Pending() {              
             // document.getElementById("MainContent_ChkPending").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorizs").checked = false;
              document.getElementById("MainContent_ChkEmergency").checked = false;
              document.getElementById("MainContent_ChkIntRece").checked = false;
              document.getElementById("MainContent_ChkIntNotReceive").checked = false;
              document.getElementById("MainContent_ChkOutsource").checked = false;
              document.getElementById("MainContent_ChkAbnormal").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;              
          }
          function ON_Completed() {
              document.getElementById("MainContent_ChkPending").checked = false;
             // document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorizs").checked = false;
              document.getElementById("MainContent_ChkEmergency").checked = false;
              document.getElementById("MainContent_ChkIntRece").checked = false;
              document.getElementById("MainContent_ChkIntNotReceive").checked = false;
              document.getElementById("MainContent_ChkOutsource").checked = false;
              document.getElementById("MainContent_ChkAbnormal").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_Tested() {
              document.getElementById("MainContent_ChkPending").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
             // document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorizs").checked = false;
              document.getElementById("MainContent_ChkEmergency").checked = false;
              document.getElementById("MainContent_ChkIntRece").checked = false;
              document.getElementById("MainContent_ChkIntNotReceive").checked = false;
              document.getElementById("MainContent_ChkOutsource").checked = false;
              document.getElementById("MainContent_ChkAbnormal").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_Authorizs() {
              document.getElementById("MainContent_ChkPending").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
             // document.getElementById("MainContent_ChkAuthorizs").checked = false;
              document.getElementById("MainContent_ChkEmergency").checked = false;
              document.getElementById("MainContent_ChkIntRece").checked = false;
              document.getElementById("MainContent_ChkIntNotReceive").checked = false;
              document.getElementById("MainContent_ChkOutsource").checked = false;
              document.getElementById("MainContent_ChkAbnormal").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_Emergency() {
              document.getElementById("MainContent_ChkPending").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorizs").checked = false;
             // document.getElementById("MainContent_ChkEmergency").checked = false;
              document.getElementById("MainContent_ChkIntRece").checked = false;
              document.getElementById("MainContent_ChkIntNotReceive").checked = false;
              document.getElementById("MainContent_ChkOutsource").checked = false;
              document.getElementById("MainContent_ChkAbnormal").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_IntRece() {
              document.getElementById("MainContent_ChkPending").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorizs").checked = false;
              document.getElementById("MainContent_ChkEmergency").checked = false;
             // document.getElementById("MainContent_ChkIntRece").checked = false;
              document.getElementById("MainContent_ChkIntNotReceive").checked = false;
              document.getElementById("MainContent_ChkOutsource").checked = false;
              document.getElementById("MainContent_ChkAbnormal").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_IntNotReceive() {
              document.getElementById("MainContent_ChkPending").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorizs").checked = false;
              document.getElementById("MainContent_ChkEmergency").checked = false;
              document.getElementById("MainContent_ChkIntRece").checked = false;
             // document.getElementById("MainContent_ChkIntNotReceive").checked = false;
              document.getElementById("MainContent_ChkOutsource").checked = false;
              document.getElementById("MainContent_ChkAbnormal").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_Outsource() {
              document.getElementById("MainContent_ChkPending").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorizs").checked = false;
              document.getElementById("MainContent_ChkEmergency").checked = false;
              document.getElementById("MainContent_ChkIntRece").checked = false;
              document.getElementById("MainContent_ChkIntNotReceive").checked = false;
             // document.getElementById("MainContent_ChkOutsource").checked = false;
              document.getElementById("MainContent_ChkAbnormal").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_Abnormal() {
              document.getElementById("MainContent_ChkPending").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorizs").checked = false;
              document.getElementById("MainContent_ChkEmergency").checked = false;
              document.getElementById("MainContent_ChkIntRece").checked = false;
              document.getElementById("MainContent_ChkIntNotReceive").checked = false;
              document.getElementById("MainContent_ChkOutsource").checked = false;
              //document.getElementById("MainContent_ChkAbnormal").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_All() {
              document.getElementById("MainContent_ChkPending").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorizs").checked = false;
              document.getElementById("MainContent_ChkEmergency").checked = false;
              document.getElementById("MainContent_ChkIntRece").checked = false;
              document.getElementById("MainContent_ChkIntNotReceive").checked = false;
              document.getElementById("MainContent_ChkOutsource").checked = false;
              document.getElementById("MainContent_ChkAbnormal").checked = false;
              //document.getElementById("MainContent_ChkAll").checked = false;
          }
          </script>
           <%-- </ContentTemplate>
          </asp:UpdatePanel>--%>
    <script language="javascript" type="text/javascript">
        function OpenReport() {
            window.open("Reports.aspx");
        }
               </script>
</asp:Content>

