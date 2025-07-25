<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="TestResultTransfer.aspx.cs" Inherits="TestResultTransfer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>

                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Test Result Transfer</h1>
                    <ol class="breadcrumb">
                      
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Test Result Transfer </li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box" style="margin-bottom:1px">
                        <div class="box-body" style="padding-bottom:0px">
                            <div class="row">
                                <div class="col-lg-3">
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
                                <div class="col-lg-3">
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
                                <div class="col-lg-3">
                                    <div class="form-group">
                                      
                                    

                             <asp:textbox id="txtPPID" class="form-control" placeholder="Select PPID No" runat="server" AutoPostBack="false">
                            </asp:textbox>
                             
                                    </div>
                                </div>
                                <div class="col-lg-3">
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
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <!--  <input type="text" class="form-control pull-right" placeholder="Enter Patient Name">-->
                                        <asp:textbox id="txtPatientName" runat="server" class="form-control pull-right" style="position: relative" tabindex="4" placeholder="Enter Patient Name">
                            </asp:textbox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                     <!--   <input type="text" class="form-control pull-right" placeholder="Enter Mobile Number"> -->
                                         <asp:textbox id="txtmobileno" runat="server" class="form-control pull-right" placeholder="Enter Ref Doc" tabindex="5">
                            </asp:textbox>
                              <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: right"
                                                        id="Div4">
                                                    </div>
                             <cc1:AutoCompleteExtender id="AutoCompleteExtender3" runat="server" MinimumPrefixLength="1" 
                            TargetControlID="txtmobileno" ServiceMethod="GetDoctor" CompletionListElementID="Div4">
                            </cc1:AutoCompleteExtender>
                                    </div>
                                </div> 
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                       <!-- <input type="text" class="form-control pull-right" placeholder="Enter Registration Number"> -->
                                        <asp:textbox id="txtregno"  runat="server" class="form-control pull-right" tabindex="5" placeholder="Enter Registration Number">
                            </asp:textbox>
                                    </div>
                                </div> 
                                 <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                       <!-- <input type="text" class="form-control pull-right" placeholder="Enter Registration Number"> -->
                                        <asp:textbox id="txtcentername_new" class="form-control" placeholder="Center Name" runat="server" AutoPostBack="false">
                            </asp:textbox>
                             <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: right"
                                                        id="Div5">
                                                    </div>
                              <cc1:AutoCompleteExtender id="AutoCompleteExtender4" runat="server" MinimumPrefixLength="1" 
                            TargetControlID="txtcentername_new" ServiceMethod="Getcenter" CompletionListElementID="Div5">
                            </cc1:AutoCompleteExtender> 
                                    </div>
                                </div>   
                                


                                <div class="col-lg-7">
                                    <div class="form-group">
                                        
                                        <asp:RadioButtonList ID="ddlStatus" runat="server" placeholder="select status" RepeatDirection="Horizontal" 
                                            AutoPostBack="True" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
    <asp:ListItem >Pending</asp:ListItem>
    <asp:ListItem>Completed</asp:ListItem>
     <asp:ListItem>Tested</asp:ListItem>
      <asp:ListItem>Authorized</asp:ListItem>
       <asp:ListItem>Emergency</asp:ListItem>
        <asp:ListItem>IntRece</asp:ListItem>
         <asp:ListItem>IntNotRece</asp:ListItem>
         <asp:ListItem>Outsource</asp:ListItem>
    <asp:ListItem >All</asp:ListItem>
   
                                        </asp:RadioButtonList>


                                       
                            <asp:sqldatasource id="sqlStatus" runat="server" connectionstring="<%$ ConnectionStrings:myconnection %>"
                                selectcommand="SELECT * FROM [DrMT]  where DrType='CC' order by DoctorName">
                            </asp:sqldatasource>
                                        </div>
                                        </div>
                                          <div class="col-lg-2">
                                    <div class="form-group"> 
                                     <asp:TextBox ID="txtbarcodeNo" placeholder="Enter Barcode No"  runat="server"  class="form-control pull-right" TabIndex="5">
                </asp:TextBox>
                             
                                    </div>
                                    </div>
                                        <div class="col-lg-2">
                                    <div class="form-group">    
                                    <asp:Label ID="Label1" runat="server" Font-Bold="true"  Text="Total Patient Count is: "></asp:Label>
<asp:Label ID="lblpcount" runat="server" Font-Bold="true" ForeColor="black" Text=""></asp:Label>
                                    </div>
                                    </div>  

                                     <div class="col-lg-1">
                                    <div class="form-group"> 
                                    <asp:button id="btnList" runat="server" onclick="btnList_Click" class="btn btn-primary" text="Click"
                                 onclientclick=" return validate();" tabindex="7" />
                                    </div>
                                    </div>  
                                           
                            </div>
                        </div>
                       
                    </div>
                     <div class="box">
                        <div class="box-body" style="margin-top:-10px">
                           <div class="table-responsive" style="width:100%">
                            <asp:gridview id="GVTestentry" runat="server"   class="table table-responsive table-sm " datakeynames="PatRegID" autogeneratecolumns="False"
                                width="100%" onrowdatabound="GVTestentry_RowDataBound" allowpaging="True" onpageindexchanging="GVTestentry_PageIndexChanging"
                                pagesize="30" 
        HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"  
 onrowediting="GVTestentry_RowEditing" onselectedindexchanged="GVTestentry_SelectedIndexChanged">
                                <columns>
                               
                                <asp:HyperLinkField HeaderText="Submit" Text="Submit" Visible="false"  DataNavigateUrlFields="PatRegID,FID" DataNavigateUrlFormatString="Addresult.aspx?PatRegID={0}&amp;FID={1}" />
 <asp:BoundField DataField="RegistratonDateTime" HeaderText=" Date" />
<asp:BoundField DataField="PatRegID" HeaderText="RegNo" />
<asp:BoundField DataField="CenterCode" HeaderText="Center"  />


  <asp:TemplateField HeaderText="FullName">
                            <ItemTemplate>
                            <asp:Label ID="lblfullname" runat="server" Text='<%#Eval("FullName") %>'></asp:Label>
                            <asp:ImageButton ID="btnEmergency"  class="flashingTextcss" ImageUrl="~/images/light-311119__340.png" runat="server"></asp:ImageButton>

                            </ItemTemplate>
                            </asp:TemplateField>

<asp:BoundField DataField="Sex" HeaderText="Gender" />
<asp:BoundField DataField="MYD" HeaderText="Age" />
<asp:BoundField DataField="DrName" HeaderText="Ref Dr" />



<asp:TemplateField HeaderText ="Test ">
    <ItemTemplate>
        <asp:Label ID="lbltestname" runat="server" Text='<%#Eval("TestName") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="SampleStatusNew" HeaderText=" Status" />


<asp:BoundField DataField="P_remark" HeaderText="Remark" />
<asp:BoundField DataField="LabRegMediPro" HeaderText="MedNo" />
<asp:BoundField DataField="Labno" HeaderText="LNo" />
    <asp:CommandField EditText="Transfer Result" HeaderText="Transfer Result"  ShowEditButton="True" />
   
   <asp:BoundField DataField="ReRunType" HeaderText="Rerun" />
   <asp:BoundField DataField="MailStatus" HeaderText=" PMail" />
   <asp:BoundField DataField="InterfaceStatus" HeaderText=" Interface" />
    <asp:BoundField DataField="PPID" HeaderText=" PPID" />
     <asp:BoundField DataField="DrMailStatus" HeaderText=" DrMail" />
                                    <asp:BoundField DataField="Isoutsource" HeaderText="Outsource" />
                                   
    
      <asp:TemplateField HeaderText="Panic">
                            <ItemTemplate>
                            <asp:Label ID="lblpanic" runat="server" Text='<%#Eval("PanicResult") %>'></asp:Label>
                            <asp:ImageButton ID="btnpanic"  class="flashingTextcss" ImageUrl="~/images/light-311119__340.png" runat="server"></asp:ImageButton>

                            </ItemTemplate>
                            </asp:TemplateField>
       <asp:BoundField DataField="SpecimanNo" HeaderText=" SrNo" />
     
       <asp:TemplateField HeaderText="ViewPres">
                            <ItemTemplate>
<asp:HyperLink ID="Hyp_viewPres" runat="server" NavigateUrl='<%# Eval("UploadPrescription") %>'>View Pres</asp:HyperLink>
                            </ItemTemplate>
                            </asp:TemplateField>
<asp:TemplateField>
    <ItemTemplate>
    <a href=""><i class="fa fa-download"    style="color:#fff"></i></a>
        <asp:HiddenField ID="hdnFID1" runat="server" Value='<%#Eval("FID") %>' /> 
        <asp:HiddenField ID="hdnopid" runat="server" Value='<%#Eval("OutsourcePatientPID") %>' /> 
        <asp:HiddenField ID="hdnORT" runat="server" Value='<%#Eval("OutResTransfer") %>' /> 
        <asp:HiddenField ID="hdnis_emergency" runat="server" Value='<%#Eval("Isemergency") %>' />
        <asp:HiddenField ID="HDPID" runat="server" Value='<%#Eval("PID") %>' /> 
         <asp:HiddenField ID="hdnPatRegID" runat="server" Value='<%#Eval("PatRegID") %>' /> 
          <asp:HiddenField ID="hdnMaindept" runat="server" Value='<%#Eval("DigModule") %>' /> 
           <asp:HiddenField ID="hdn_Maintestname" runat="server" Value='<%#Eval("TestName") %>' /> 
           <asp:HiddenField ID="hdn_MTcode" runat="server" Value='<%#Eval("MTCode") %>' /> 
            
    </ItemTemplate>
</asp:TemplateField>


</columns>


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
</asp:Content>

