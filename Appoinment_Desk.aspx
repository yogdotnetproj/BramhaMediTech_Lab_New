 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="Appoinment_Desk.aspx.cs" Inherits="Appoinment_Desk" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:scriptmanager Id="scriptmanager" runat="server">
    </asp:scriptmanager>
  
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Appoinment Book</h1>
                    <%--<ol class="breadcrumb">
                    <li><a href="Login.aspx"><i class="fa fa-fw fa-lock"></i> Login</a></li>
                    <li><a href="Login.aspx"><i class="fa fa-fw fa-power-off"></i> Log out</a></li>
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">IPD Desk</li>
                    </ol>--%>
                </section>
                <!-- Main content -->
                <section class="content">               
                                        
                
                         <div class="box" runat="server" id="Panel3">

                             <div class="box-header">
                                    <asp:Label ID="lblMessage" class="red pull-center"  runat="server" Text="" Font-Bold="true" ForeColor="green" ></asp:Label>
                          
                                   </div>
                            
                              
                                 <div class="box-body">  
                                     <div class="row">
                              
                                         <div id="Div3" class="col-lg-12" runat="server" >                                       
                                    <div class ="row"> 
                                        <div class="col-lg-2 text-right">
                                                    <div class ="form-group">
                                                        <asp:Label ID="Label2" runat="server" ForeColor="Black" Font-Bold="true" Text="Appoinment:" ></asp:Label>
                                                        </div>

                                        </div>
                                        <div class="col-lg-4 text-left">
                                                    <div class ="form-group">
                                                         <asp:ImageButton ID="btnAdmit1" runat="server" Width="50px" ImageUrl="~/Images0/Admit.png" ToolTip="Appoinment"   
                                                               Text="Appoinment" />
                                                        </div>
                                            </div>
                                        <div class="col-lg-3 text-right">
                                                    <div class ="form-group">
                                                        <asp:Label ID="Label3" runat="server" ForeColor="Black" Font-Bold="true" Text="Booked Appoinment:" ></asp:Label>
                                                        </div>

                                        </div>
                                        <div class="col-lg-3 text-left">
                                                    <div class ="form-group">
                                                        <asp:ImageButton ID="ImageButton1" runat="server"  Width="35px" ToolTip="Booked Appoinment" ImageUrl="~/Images0/images1.jpg" 
                                                               Text="Booked Appoinment" />
                                                        </div>
                                            </div>
                                       
                                    </div>   
                                             </div>
                                         </div>
                                          <div id="Div1" class="col-lg-12" runat="server" >
                                       
                                    <div class ="row">  

                                        <div class="col-lg-3 text-left">
                                                    <div class ="form-group">

                                                       <asp:dropdownlist id="ddlCenter" runat="server" class="form-control" tabindex="3">
                                                              </asp:dropdownlist>
                                                    </div>
                                         
                                                    </div>
                                                        
                                       
                                         <div class="col-lg-2">
                                                    <div class ="form-group">
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
                                        <div class="col-lg-2">
                                                    <div class ="form-group">
                                                       
                                                        
                                                         <asp:Button ID="btnSearch" runat="server" Text="Search"  class="btn btn-primary btnSearch" OnClick="btnSearch_Click" />
                                                    </div>
                                                        
                                        </div>
                                        <div class="col-lg-4">
                                                    <div class ="form-group">
                                                        <asp:Label ID="Lmsg" runat="server" ForeColor="Red" Text="" Font-Bold="true" ></asp:Label>
                                                    </div>
                                                        
                                        </div>
                                              </div>
                                              </div>
                                    
                                   <div class="col-lg-12" runat="server" visible="false" >
                                       
                                    <div class ="row">  
                                        
                                       
                                               <div class="col-lg-12">
                                                    <div class ="form-group"  style="border:solid;color:orange;background-color:white;width:1200px;height:90px;">
                                   
                                                       
                                                                         
                                                                         <asp:RadioButtonList ID="RdbRoomType" runat="server" ForeColor="Black" Font-Size="Larger" Font-Bold="true"  RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RdbRoomType_SelectedIndexChanged" RepeatColumns="9"  >
                                                             
                                                                             </asp:RadioButtonList>  
                                                                        
                                                        </div>
                                               </div>
                                        </div>
                                       </div>
                                          <div class="col-lg-12" ">
                                    <div class ="row">                                  
                                      
                                        
                     <div class="box" >
                                    <div class="box-body" >

                                   
                   
                            <div  class="col-lg-12" style="border:thin;color:orange;width:1050px" >
                         <div class="row"> 
                             <asp:DataList ID="BedDataList" runat="server" Width="1050px"   RepeatColumns="9" RepeatDirection="Horizontal" OnEditCommand="BedDataList_EditCommand" OnItemDataBound="BedDataList_ItemDataBound" OnItemCommand="BedDataList_ItemCommand">
                                 <ItemStyle  Font-Bold="True" BorderColor="#0066FF" BorderStyle="Solid" BorderWidth="1px" />
                                 <ItemTemplate>
                                     <div class="box">
                                         <div class="box-body">
                                             <div class="col-lg-12" style="border:thin;color:black;width:70px;height:40px">
                                                 <div class="row">
                                                     <div class="col-lg-1 text-left" style="width:100px">
                                                         <div class="form-group">
                                                             <asp:Label ID="lblBedName" Font-Bold="true" runat="server" Text='<%# Eval("TimeSlot") %>' />
                                                            <asp:HiddenField ID="hdnstatus" runat="server" Value='<%# Eval("Status") %>' />
                                                               <asp:HiddenField ID="hdnStartTime" runat="server" Value='<%# Eval("StartTime") %>' />
                                                            <%--  <asp:HiddenField ID="hdnIpdNo" runat="server" Value='<%# Eval("IpdNo") %>' /> 
                                                              <asp:HiddenField ID="hdnIpdId" runat="server" Value='<%# Eval("IpdId") %>' />                                                              
                                                             <asp:Label ID="lblIsAdmited" runat="server" Visible="false" Text='<%# Eval("PatStatus") %>' />
                                                              <asp:HiddenField ID="hdn_IsUniversalPrecaution" runat="server" Value='<%# Eval("IsUniversalPrecaution") %>' /> 
                                                                <asp:Label ID="lblRegId" runat="server" Visible="false" Text = '<%# Eval("PatRegId") %>' /> 
                                                              <asp:Label ID="lblPatientName" runat="server" Width="200px" Text='<%# Eval("FullName") %>' />--%>
                                                          
                                                              
                                                              </div>
                                                     </div>
                                                     </div>
                                                
                                                    
                                                     <div class="row">
                                                         <div class="col-lg-1 text-left" style="width:70px" >
                                                            
                                                             
                                                              <asp:ImageButton ID="btnAdmit" runat="server" Width="50px" ImageUrl="~/Images0/Admit.png" ToolTip=" Book" CommandName="Edit"  
                                                               Text="Add" />
                                                            <asp:ImageButton ID="btnBooked" runat="server" Visible="false" Width="35px" ImageUrl="~/Images0/images1.jpg" ToolTip=" Booked" CommandName="Booked"  
                                                               Text="Booked" />
                                                            
                                                         </div>
                                                     </div>
                                                
                                             </div>
                                         </div>
                                     </div>
                                 </ItemTemplate>
                             </asp:DataList>
                         </div>
                         </div>
                                                        
                <div  class="col-lg-12" >
                         <div class="row">
                             </div>
                   </div>

                   </div>
                         </div>

               
                                        </div>
                                              </div>
                                        
                                        
                                     </div>
                                     </div>

                                         
                                     
                               
               
                 
                                 
                 
                        </section>
                <!-- /.content -->
            
             
        <script type="text/javascript">
            //Date picker
            $('#fromdate, #todate').datepicker({
                autoclose: true
            })
        </script>
        <!-- ./wrapper -->
         
         <script language="javascript" type="text/javascript">
             function OpenReport() {
                 window.open("Reports.aspx");
             }
               </script>
       </asp:Content>
