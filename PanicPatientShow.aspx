 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="PanicPatientShow.aspx.cs" Inherits="PanicPatientShow" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
   
            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Panic patient result</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Panic patient result</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
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
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
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
                                       
                                        
                                            
                                            <asp:textbox id="txttestname" placeholder="select Dept Name" class="form-control" runat="server" >
                            </asp:textbox>
                            <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: right"
                                                        id="Div3">
                                                    </div>
                                         <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1" 
                            TargetControlID="txttestname" ServiceMethod="GetDeptName" CompletionListElementID="Div3">
                            </cc1:AutoCompleteExtender>
                                        
                                      
                                      

                                    </div>
                                </div>
                                </div>
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <!--  <input type="text" class="form-control pull-right" placeholder="Enter Patient Name">-->
                                        <asp:textbox id="txtPatientName" runat="server" class="form-control pull-right" style="position: relative" tabindex="4" placeholder="Enter Patient Name">
                            </asp:textbox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
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
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       <!-- <input type="text" class="form-control pull-right" placeholder="Enter Registration Number"> -->
                                        <asp:textbox id="txtregno"  runat="server" class="form-control pull-right" tabindex="5" placeholder="Enter Registration Number">
                            </asp:textbox>
                                    </div>
                                </div> 
                                 <div class="col-sm-3">
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
                                </div>

                            <div class="row mb-3">
                                <div class="col-sm-7">
                                    <div class="form-group">
                                        
                                        <asp:RadioButtonList ID="ddlStatus" runat="server" placeholder="select status" RepeatDirection="Horizontal" 
                                          CssClass="form-check" AutoPostBack="True" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
    <asp:ListItem >Pending</asp:ListItem>
    <asp:ListItem>Inform</asp:ListItem>
     
    <asp:ListItem Selected="True" >All</asp:ListItem>
   
                                        </asp:RadioButtonList>


                                       
                            <asp:sqldatasource id="sqlStatus" runat="server" connectionstring="<%$ ConnectionStrings:myconnection %>"
                                selectcommand="SELECT * FROM [DrMT]  where DrType='CC' order by DoctorName">
                            </asp:sqldatasource>
                                        </div>
                                        </div>
                                          <div class="col-sm-2">
                                    <div class="form-group"> 
                                     <asp:TextBox ID="txtbarcodeNo" placeholder="Enter Barcode No"  runat="server"  class="form-control pull-right" TabIndex="5">
                </asp:TextBox>
                             
                                    </div>
                                    </div>
                                        <div class="col-sm-2">
                                    <div class="form-group">    
                                    <asp:Label ID="Label1" runat="server" class="btn btn-sm btn-primary" Font-Bold="true" ForeColor="white" Text="Total Patient Count is: "></asp:Label>
<asp:Label ID="lblpcount" runat="server" Font-Bold="true" ForeColor="black" Text=""></asp:Label>
                                    </div>
                                    </div>  

                                     <div class="col-sm-1">
                                    <div class="form-group"> 
                                    <asp:button id="btnList" runat="server" onclick="btnList_Click" class="btn btn-primary" text="Click"
                                 onclientclick=" return validate();" tabindex="7" />
                                    </div>
                                    </div>  
                                           
                            </div>
                        </div>
                       
                    </div>
                     <div class="box">
                        <div class="box-body">
                           <div class="table-responsive" style="width:100%">
                            <asp:gridview id="GVTestentry" runat="server"   
                                   class="table table-responsive table-sm " datakeynames="PatRegID" autogeneratecolumns="False"
                                width="100%" onrowdatabound="GVTestentry_RowDataBound" allowpaging="True" onpageindexchanging="GVTestentry_PageIndexChanging"
                                pagesize="30" 
        HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"  
 onrowediting="GVTestentry_RowEditing" onselectedindexchanged="GVTestentry_SelectedIndexChanged" 
                                   onrowdeleting="GVTestentry_RowDeleting">
                                <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                                <columns>
                               

 <asp:BoundField DataField="RegistratonDateTime" HeaderText=" Date" />
<asp:BoundField DataField="PatRegID" HeaderText="Reg.No" />
<asp:BoundField DataField="CenterCode" HeaderText="Center"  />


  <asp:TemplateField HeaderText="FullName">
                            <ItemTemplate>
                            <asp:Label ID="lblfullname" runat="server" Text='<%#Eval("FullName") %>'></asp:Label>
                            <asp:ImageButton ID="btnEmergency"  class="flashingTextcss" ImageUrl="~/images/light-311119__340.png" runat="server"></asp:ImageButton>

                            </ItemTemplate>
                            </asp:TemplateField>

<asp:BoundField DataField="Sex" HeaderText="Gender" />
<asp:BoundField DataField="MYD" HeaderText="Age" />
<asp:BoundField DataField="DrName" HeaderText="Ref Doc" />



<asp:TemplateField HeaderText ="Test ">
    <ItemTemplate>
        <asp:Label ID="lbltestname" ForeColor="Red" runat="server" Text='<%#Eval("TestName") %>'></asp:Label>
         <asp:ImageButton ID="btnpanic"  class="flashingTextcss" ImageUrl="~/images/light-311119__340.png" runat="server"></asp:ImageButton>

    </ItemTemplate>
</asp:TemplateField>




    <asp:CommandField DeleteText="Report" HeaderText="Report"  ShowDeleteButton="True" />
   
   
   <asp:BoundField DataField="MailStatus" HeaderText=" Mail Status" />
   
    <asp:BoundField DataField="PPID" HeaderText=" PPID" />
     <asp:BoundField DataField="DrMailStatus" HeaderText=" Dr Mail Status" />
    
      <asp:TemplateField HeaderText="Inform To">
                            <ItemTemplate>
                            <asp:TextBox ID="txtinformto" runat="server" Text='<%#Eval("PanicInformToResult") %>'></asp:TextBox>
                           
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate>
                            <asp:TextBox ID="txtinformremark" runat="server" Text='<%#Eval("PanicRemark") %>'></asp:TextBox>
                           
                            </ItemTemplate>
                            </asp:TemplateField>
                               <asp:CommandField EditText="Action" HeaderText="Action"  ShowEditButton="True" />
                           
     
<asp:TemplateField>
    <ItemTemplate>
    <a href=""><i class="fa fa-download"    style="color:#fff"></i></a>
        <asp:HiddenField ID="hdnFID1" runat="server" Value='<%#Eval("FID") %>' /> 
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
          
            <!-- /.content-wrapper -->
           
        <script language ="javascript" type ="text/javascript" >
            function jsPopup(jsURL) {
                var hdl; if (jsURL != "") {

                    var jsoption = "scrollbars=yes,resizable=yes,width=500,height=500,left=100,top=100,status=yes";
                    hdl = window.open(jsURL, "win01", jsoption);

                }

            }

 

</script>
<!--<script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>-->
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