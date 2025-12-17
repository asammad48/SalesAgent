import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import DashboardLayout from './layout/DashboardLayout';
import LeadsPage from '@/pages/leads/LeadsPage';
import SalesTasksPage from '@/pages/sales-tasks/SalesTaskMonitorPage';
import AiControlPage from '@/pages/ai-control/AiControlPage';
import ConfigurationPage from '@/pages/configuration/ConfigurationPage';
import AnalyticsPage from '@/pages/analytics/AnalyticsPage';
import LeadDetailPage from '@/pages/leads/LeadDetailPage';

const router = createBrowserRouter([
  {
    path: '/',
    element: <DashboardLayout><LeadsPage /></DashboardLayout>,
  },
  {
    path: '/leads',
    element: <DashboardLayout><LeadsPage /></DashboardLayout>,
  },
  {
    path: '/leads/:leadId',
    element: <DashboardLayout><LeadDetailPage /></DashboardLayout>,
  },
  {
    path: '/sales-tasks',
    element: <DashboardLayout><SalesTasksPage /></DashboardLayout>,
  },
  {
    path: '/ai-control',
    element: <DashboardLayout><AiControlPage /></DashboardLayout>,
  },
  {
    path: '/configuration',
    element: <DashboardLayout><ConfigurationPage /></DashboardLayout>,
  },
  {
    path: '/analytics',
    element: <DashboardLayout><AnalyticsPage /></DashboardLayout>,
  },
]);

const AppRouter = () => <RouterProvider router={router} />;

export default AppRouter;
