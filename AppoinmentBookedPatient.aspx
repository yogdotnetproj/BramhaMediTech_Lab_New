 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="AppoinmentBookedPatient.aspx.cs" Inherits="AppoinmentBookedPatient" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>

                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Appoinment Booked Patient</h1>
                    <ol class="breadcrumb">
                
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Appoinment Booked Patient</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    

                    <div  class="regular">
    

    <div class="row">
                                <div class="col-lg-3">
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
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-lg-3">
                                    <div class="form-group">
                                        
                                     
                                         <asp:TextBox ID="Txt_Patientname" TabIndex="1" runat="server" placeholder="Enter Patient Name" class="form-control" 
                                            AutoPostBack="false"></asp:TextBox>
                                       
                                    </div>
                                </div>
         <div class="col-lg-3">
                                    <div class="form-group">
                                        
                                     
                                         <asp:TextBox ID="txtappNo" TabIndex="1" runat="server" placeholder="Enter App No" class="form-control" 
                                            AutoPostBack="false"></asp:TextBox>
                                       
                                    </div>
                                </div>
                                 <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12" style="text-align:center" >
<asp:RadioButtonList ID="RblCallStatus" runat="server" RepeatDirection="Horizontal">
    <asp:ListItem>Done</asp:ListItem>
    
    
      <asp:ListItem Selected="True">Not Done</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                </div>
                                </div>
                                </div>
                                 <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   
                                     <asp:Button ID="btnclick" runat="server"  class="btn btn-primary" 
                    Text="Click" TabIndex="7" onclick="btnclick_Click" />
                                    <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red"
            Height="23px"   Text="" Width="525px" ></asp:Label>
                                </div> 
                            </div>
                        </div>
      <div class="box">
                        <div class="box-body">
                           <div class="table-responsive" style="width:100%">
                            <asp:gridview id="GVTestentry" runat="server"   class="table table-responsive table-sm " datakeynames="AppId" autogeneratecolumns="False"
                                width="100%" onrowdatabound="GVTestentry_RowDataBound" allowpaging="True" onpageindexchanging="GVTestentry_PageIndexChanging"
                                pagesize="50" 
        HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"  
 onrowediting="GVTestentry_RowEditing" onselectedindexchanged="GVTestentry_SelectedIndexChanged">
                                <columns>
                               
   <asp:BoundField DataField="AppId" HeaderText=" App ID" />                          
<asp:BoundField DataField="AppDate" HeaderText=" Date" />
<asp:BoundField DataField="PatientName" HeaderText="Patient Name" />
<asp:BoundField DataField="CenterCode" HeaderText="Center"  />

<asp:BoundField DataField="AppoinmentTime" HeaderText=" App Time" />

<asp:BoundField DataField="Age" HeaderText="Age" />
<asp:BoundField DataField="AgeType" HeaderText="AgeType" />
<asp:BoundField DataField="Gender" HeaderText="Gender" />
<asp:BoundField DataField="Phone" HeaderText="Phone No" />

<asp:BoundField DataField="Pataddress" HeaderText="Address" />

<asp:BoundField DataField="AppointAttend" HeaderText="Status" />

<asp:CommandField EditText="Attend" ItemStyle-ForeColor="Black" HeaderText="Action"  ShowEditButton="True" />



</columns>


                            </asp:gridview>
                             <div class="Pager"></div>
                          </div>
                        </div>
                    </div>

        <asp:Label ID="Label44" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red"
            Height="23px"   Text="Label" Width="525px" Visible="False"></asp:Label><br />
      
     
       

        </div>


                </section>
                <!-- /.content -->
          
        <!-- ./wrapper -->
        
        <script type="text/javascript">
            //Date picker
            $('#fromdate, #todate').datepicker({
                autoclose: true
            })
        </script>
      


    </asp:Content>