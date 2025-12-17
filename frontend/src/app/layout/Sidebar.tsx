import React from 'react';
import { NavLink } from 'react-router-dom';

const Sidebar: React.FC = () => {
  return (
    <div className="w-64 bg-white dark:bg-gray-800 border-r border-gray-200 dark:border-gray-700">
      <div className="h-16 flex items-center justify-center">
        <h1 className="text-2xl font-bold text-gray-800 dark:text-white">AI Sales Agent</h1>
      </div>
      <nav className="mt-6">
        <NavLink to="/leads" className="flex items-center mt-4 py-2 px-6 text-gray-700 dark:text-gray-200 hover:bg-gray-200 dark:hover:bg-gray-700">
          Leads
        </NavLink>
        <NavLink to="/sales-tasks" className="flex items-center mt-4 py-2 px-6 text-gray-700 dark:text-gray-200 hover:bg-gray-200 dark:hover:bg-gray-700">
          Sales Tasks
        </NavLink>
        <NavLink to="/ai-control" className="flex items-center mt-4 py-2 px-6 text-gray-700 dark:text-gray-200 hover:bg-gray-200 dark:hover:bg-gray-700">
          AI Control
        </NavLink>
        <NavLink to="/configuration" className="flex items-center mt-4 py-2 px-6 text-gray-700 dark:text-gray-200 hover:bg-gray-200 dark:hover:bg-gray-700">
          Configuration
        </NavLink>
        <NavLink to="/analytics" className="flex items-center mt-4 py-2 px-6 text-gray-700 dark:text-gray-200 hover:bg-gray-200 dark:hover:bg-gray-700">
          Analytics
        </NavLink>
      </nav>
    </div>
  );
};

export default Sidebar;
