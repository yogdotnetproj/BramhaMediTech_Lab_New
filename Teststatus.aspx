<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Teststatus.aspx.cs" Inherits="Teststatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <Triggers>
               
         
              <asp:PostBackTrigger ControlID="btnreport" />
                <asp:PostBackTrigger ControlID="btnPdf" />

           </Triggers>
        <ContentTemplate>
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Daily Patient Test Status </h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div> 
                                          <!--  <input type="text" class="form-control pull-right" id="fromdate"> -->  
                                            <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>   
                                       
                           
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                           <!-- <input type="text" class="form-control pull-right" id="todate">-->
                                            <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                      
                                        <asp:dropdownlist id="drp_deparment" runat="server" class="form-control form-select" tabindex="6">
                                </asp:dropdownlist>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       <!-- <select class="form-control">
                                            <option>All</option>
                                            <option>Another Option</option>
                                            <option>Other Option</option>
                                        </select> -->
                                         <asp:dropdownlist id="ddlStatus" runat="server" Visible="false" class="form-control form-select" tabindex="3">
                                    <asp:listitem text="All Status" value="0">
                                    </asp:listitem>
                                   
                                    <asp:listitem text="Registered" value="Registered">
                                    </asp:listitem>
                                    <asp:listitem text="Tested" value="Tested">
                                    </asp:listitem>
                                    <asp:listitem text="Authorized" value="Authorized">
                                    </asp:listitem>
                                </asp:dropdownlist>
                                    </div>
                                </div>
                                </div>
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       <!-- <input type="text" class="form-control pull-right" placeholder="Enter Patient Name">-->
                                        <asp:TextBox id="txtPatientName" tabIndex=4 runat="server" placeholder="Enter Patient Name"
class="form-control pull-right" AutoPostBack="false" ></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        
                                       
                                        <asp:TextBox ID="txtmobileno" class="form-control pull-right" placeholder="Enter Mobile Number"  tabindex="5" runat="server"></asp:TextBox>
                                    </div>
                                </div> 
                                
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        
                                        <asp:DropDownList ID="ddlcenter" Visible="false"  class="form-control form-select" runat="server" autopostback="True" 
                                 onselectedindexchanged="ddlcenter_SelectedIndexChanged" >
                             </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        
                                         <asp:TextBox ID="txtregno" runat="server"  placeholder="Enter Registration Number"
                                   class="form-control pull-right" >
                                </asp:TextBox>
                                    </div>
                                </div>  
                                
                                
                                              </div>
                               
                                                      <div class="row mb-3">
                                                          <div class="col-sm-5">
                                    <div class="form-group">
                                      
                                        
                                         <asp:TextBox ID="txttests"  runat="server" 
                                   class="form-control pull-right" AutoPostBack="True" placeholder="Enter Test Name"
                                            ontextchanged="txttests_TextChanged" ></asp:TextBox>
                                 <div style="display: none; overflow: scroll; width: 348px; height: 120px" id="div">
                                    </div>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                        CompletionListElementID="div" ServiceMethod="FillTests"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" TargetControlID="txttests"
                                        MinimumPrefixLength="1">
                                    </cc1:AutoCompleteExtender>
                                    </div>
                                </div>  
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        
                                         <asp:TextBox ID="txtDoctorName"  placeholder="Enter Ref Doc" ontextchanged="txtDoctorName_TextChanged" runat="server" 
                                   class="form-control pull-right" ></asp:TextBox>
                                 <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: left"
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
                                                          <div class="col-sm-4 text-right">
                                                                <asp:Button ID="btnList" class="btn btn-success" runat="server" onclick="btnList_Click" 
                                    OnClientClick="return validate();"  tabindex="8" text="Submit" />
                                     <asp:Button ID="btnreport" class="btn btn-info" runat="server" 
                                    OnClientClick="return validate();"  tabindex="9" text="Report" 
                                        onclick="btnreport_Click" />
                                                               <asp:Button ID="btnPdf" class="btn btn-secondary" runat="server" 
                                    OnClientClick="return validate();"  tabindex="10" text="PDF Report" OnClick="btnPdf_Click"             />
                                                              </div>

                                                      </div>
                                
                                
                                                   <div class="col-lg-12">
                                    <div class="form-group form-check">
                                        
                                        <asp:RadioButtonList ID="ddlStatusAll" runat="server" Visible="false" placeholder="select status" RepeatDirection="Horizontal" 
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlStatusAll_SelectedIndexChanged" >
    <asp:ListItem>Registered</asp:ListItem>   
    <asp:ListItem>SamCollect</asp:ListItem> 
     <asp:ListItem>SamAccept</asp:ListItem>
         <asp:ListItem>SamReject</asp:ListItem>
     <asp:ListItem>Tested</asp:ListItem>
      <asp:ListItem>Authorized</asp:ListItem>
       <asp:ListItem>Completed</asp:ListItem>
      
       
         <asp:ListItem>Dispatch</asp:ListItem>
    <asp:ListItem Selected="True" >All</asp:ListItem>
   
                                        </asp:RadioButtonList>
                                         <div class="form-group pl-2 d-flex">
                                         <div class="col">
                                    <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" runat="server"  onclick="ON_Registered()" id="ChkRegistered"/>
                                      <label class="form-check-label w-8" for="flexSwitchCheckDefault">Registered</label>
                                    </div>
                                        </div>
                                         <div class="col">
                                    <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" runat="server"  onclick="ON_SamCollect()" id="ChkSamCollect"/>
                                      <label class="form-check-label w-8" for="flexSwitchCheckDefault">SamCollect</label>
                                    </div>
                                        </div>
                                         <div class="col">
                                    <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" runat="server"  onclick="ON_SamAccept()" id="ChkSamAccept"/>
                                      <label class="form-check-label w-8" for="flexSwitchCheckDefault">SamAccept</label>
                                    </div>
                                        </div>
                                         <div class="col">
                                    <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" runat="server"  onclick="ON_SamReject()" id="ChkSamReject"/>
                                      <label class="form-check-label w-8" for="flexSwitchCheckDefault">SamReject</label>
                                    </div>
                                        </div>
                                         <div class="col">
                                    <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" runat="server"  onclick="ON_Tested()" id="ChkTested"/>
                                      <label class="form-check-label w-8" for="flexSwitchCheckDefault">Tested</label>
                                    </div>
                                        </div>
                                         <div class="col">
                                    <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" runat="server"  onclick="ON_Authorized()" id="ChkAuthorized"/>
                                      <label class="form-check-label w-8" for="flexSwitchCheckDefault">Authorized</label>
                                    </div>
                                        </div>
                                         <div class="col">
                                    <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" runat="server"  onclick="ON_Completed()" id="ChkCompleted"/>
                                      <label class="form-check-label w-8" for="flexSwitchCheckDefault">Completed</label>
                                    </div>
                                        </div>
                                         <div class="col">
                                    <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" runat="server"  onclick="ON_Dispatch()" id="ChkDispatch"/>
                                      <label class="form-check-label w-8" for="flexSwitchCheckDefault">Dispatch</label>
                                    </div>
                                        </div>
                                         <div class="col">
                                    <div class="form-check form-switch">
                                      <input class="form-check-input" type="checkbox" runat="server" checked="checked"  onclick="ON_All()" id="ChkAll"/>
                                      <label class="form-check-label w-8" for="flexSwitchCheckDefault">All</label>
                                    </div>
                                        </div>
                                       </div>
                          
                                        </div>
                                        </div>
                            </div>
                        </div>
                        
                       
                     
                  
                    <div class="box">
                   <!--  <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   
                                  RL
                                </div> 
                            </div>
                        </div>-->

                        <div class="box-header with-border text-center mt-2 mb-2">
<asp:Label ID="LblPcount" runat="server" Text="" style="color:#38c8dd;font-size:18px" Font-Bold="true"></asp:Label>
                           <!--  <h4><span class="text-red"> Patients</span> found with selected criteria</h4>-->
                        </div>
                        <div class="box-body">
                              <div class="table-responsive" style="width:100%">
                <asp:gridview id="gridreport" runat="server" class="table table-responsive table-sm table-bordered" autogeneratecolumns="False" onpageindexchanging="gridreport_PageIndexChanging"
                    onrowdatabound="gridreport_RowDataBound" 
        HeaderStyle-ForeColor="Black"
    pagesize="4000" skinid="GridSample" AllowPaging="true" width="100%" 
                                      onrowcreated="gridreport_RowCreated">
                    <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                    <columns>
                     <asp:BoundField DataField="Patregdate" HeaderText="Date " >
                                 <ItemStyle Width="150px" />
                             </asp:BoundField>
                            <asp:BoundField DataField="PatRegID" HeaderText="RegNo" SortExpression="RegNo" />
                         
                            <asp:TemplateField HeaderText="Full Name">
                            <ItemTemplate>
                            <asp:Label ID="lblfullname" runat="server" Text='<%#Eval("NAME") %>'></asp:Label>
                            <asp:ImageButton ID="btnEmergency" Visible="false"  class="flashingTextcss" ImageUrl="~/images/light-311119__340.png" runat="server"></asp:ImageButton>

                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="sex" HeaderText="Gender" SortExpression="Sex" />
                            <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                            <asp:BoundField DataField="MDY" HeaderText="MDY" SortExpression="Mdy" />
                            <asp:BoundField DataField="Drname" HeaderText="Ref Dr " SortExpression="Drname" />
                            <asp:BoundField DataField="CenterName" HeaderText="Center " SortExpression="CenterName" />
                            <asp:BoundField DataField="Maintestname" HeaderText="Test " SortExpression="Maintestname">
                                <ItemStyle Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" />  
                            <asp:BoundField DataField="TestCharges" HeaderText=" Charges" SortExpression="TestCharges" >
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>

                        <asp:BoundField DataField="PaidAmt" HeaderText=" Rec Amt" SortExpression="PaidAmt" >
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>
                        <asp:BoundField DataField="DiscAmt" HeaderText=" Disc Amt" SortExpression="DiscAmt" >
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>

                            <asp:BoundField DataField="OutStAmt" HeaderText="Balance" SortExpression="OutStAmt" >
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:BoundField>    
                                        <asp:BoundField DataField="MailStatus" HeaderText=" MailStatus" />  
                                          <asp:BoundField DataField="SpecimanNo" Visible="false" HeaderText=" SpecimanNo" />  
                          <asp:BoundField DataField="PPID" HeaderText=" PPID" />  
                        <asp:TemplateField HeaderText="ViewPres">
                            <ItemTemplate>
<asp:HyperLink ID="Hyp_viewPres" runat="server" target="_blank" style="color:White;font-family: MyriadPro,Arial, Helvetica, sans-serif;text-decoration:underline; font-size: small;  font-weight: bold;padding-left:10px;" NavigateUrl='<%# Eval("UploadPrescription") %>'>View Pres</asp:HyperLink>
                            </ItemTemplate>
                            </asp:TemplateField>
                                           <asp:BoundField DataField="CreatedBy" HeaderText=" Created By" />    
                        <asp:BoundField DataField="Patphoneno" HeaderText=" Phone No" /> 
                        <asp:BoundField DataField="Pataddress" HeaderText="Address" />   
                         <asp:BoundField DataField="WhatAppReport" HeaderText=" WhatAppStatus" />            
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnFID" runat="server" Value='<%#Eval("FID") %>' />
                                      <asp:HiddenField ID="isemergency" runat="server" Value='<%#Eval("Isemergency") %>' />
                                      <asp:HiddenField ID="hdnisphebocollect" runat="server" Value='<%#Eval("PhlebotomistCollect") %>' />
                                    <asp:HiddenField ID="hdnSpecimanNo" runat="server" Value='<%#Eval("SpecimanNo") %>' />
                                     <asp:HiddenField ID="hdntestcharges" runat="server" Value='<%#Eval("testcharges") %>' />
                                     <asp:HiddenField ID="hdprintstatus" runat="server" Value='<%#Eval("Patrepstatus") %>' />
                                    <asp:HiddenField ID="hdn_ISpheboAccept" runat="server" Value='<%#Eval("ISpheboAccept") %>' />
                                    <asp:HiddenField ID="hdn_PatStatus" runat="server" Value='<%#Eval("status") %>' />
                                     <asp:HiddenField ID="hdn_UploadPrescription" runat="server" Value='<%#Eval("UploadPrescription") %>' />
                                     <asp:HiddenField ID="hdnWhStatus" runat="server" Value='<%#Eval("WhatAppReport") %>' />
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

      <script language="javascript" type="text/javascript">

          function ON_Registered() {
              // document.getElementById("MainContent_ChkRegistered").checked = false;
              document.getElementById("MainContent_ChkSamCollect").checked = false;
              document.getElementById("MainContent_ChkSamAccept").checked = false;
              document.getElementById("MainContent_ChkSamReject").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorized").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkDispatch").checked = false;             
              document.getElementById("MainContent_ChkAll").checked = false;              
          }
          function ON_SamCollect() {
               document.getElementById("MainContent_ChkRegistered").checked = false;
              //document.getElementById("MainContent_ChkSamCollect").checked = false;
              document.getElementById("MainContent_ChkSamAccept").checked = false;
              document.getElementById("MainContent_ChkSamReject").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorized").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkDispatch").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_SamAccept() {
              document.getElementById("MainContent_ChkRegistered").checked = false;
              document.getElementById("MainContent_ChkSamCollect").checked = false;
             // document.getElementById("MainContent_ChkSamAccept").checked = false;
              document.getElementById("MainContent_ChkSamReject").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorized").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkDispatch").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_SamReject() {
              document.getElementById("MainContent_ChkRegistered").checked = false;
              document.getElementById("MainContent_ChkSamCollect").checked = false;
              document.getElementById("MainContent_ChkSamAccept").checked = false;
             // document.getElementById("MainContent_ChkSamReject").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorized").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkDispatch").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_Tested() {
              document.getElementById("MainContent_ChkRegistered").checked = false;
              document.getElementById("MainContent_ChkSamCollect").checked = false;
              document.getElementById("MainContent_ChkSamAccept").checked = false;
              document.getElementById("MainContent_ChkSamReject").checked = false;
             // document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorized").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkDispatch").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_Authorized() {
              document.getElementById("MainContent_ChkRegistered").checked = false;
              document.getElementById("MainContent_ChkSamCollect").checked = false;
              document.getElementById("MainContent_ChkSamAccept").checked = false;
              document.getElementById("MainContent_ChkSamReject").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
             // document.getElementById("MainContent_ChkAuthorized").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkDispatch").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_Completed() {
              document.getElementById("MainContent_ChkRegistered").checked = false;
              document.getElementById("MainContent_ChkSamCollect").checked = false;
              document.getElementById("MainContent_ChkSamAccept").checked = false;
              document.getElementById("MainContent_ChkSamReject").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorized").checked = false;
             // document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkDispatch").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_Dispatch() {
              document.getElementById("MainContent_ChkRegistered").checked = false;
              document.getElementById("MainContent_ChkSamCollect").checked = false;
              document.getElementById("MainContent_ChkSamAccept").checked = false;
              document.getElementById("MainContent_ChkSamReject").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorized").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
             // document.getElementById("MainContent_ChkDispatch").checked = false;
              document.getElementById("MainContent_ChkAll").checked = false;
          }
          function ON_All() {
              document.getElementById("MainContent_ChkRegistered").checked = false;
              document.getElementById("MainContent_ChkSamCollect").checked = false;
              document.getElementById("MainContent_ChkSamAccept").checked = false;
              document.getElementById("MainContent_ChkSamReject").checked = false;
              document.getElementById("MainContent_ChkTested").checked = false;
              document.getElementById("MainContent_ChkAuthorized").checked = false;
              document.getElementById("MainContent_ChkCompleted").checked = false;
              document.getElementById("MainContent_ChkDispatch").checked = false;
             // document.getElementById("MainContent_ChkAll").checked = false;
          }
           </script>
            </ContentTemplate>
           </asp:UpdatePanel>
</asp:Content>

