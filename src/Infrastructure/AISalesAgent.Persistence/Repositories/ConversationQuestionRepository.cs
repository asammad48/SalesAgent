using AISalesAgent.Application.Interfaces;
using AISalesAgent.Domain.Entities;
using AISalesAgent.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AISalesAgent.Infrastructure.Persistence.Repositories
{
    public class ConversationQuestionRepository : Repository<ConversationQuestion>, IConversationQuestionRepository
    {
        public ConversationQuestionRepository(SalesAgentDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ConversationQuestion>> GetActiveQuestionsAsync()
        {
            return await _context.ConversationQuestions.Where(q => q.IsActive).ToListAsync();
        }

        public async Task<IEnumerable<ConversationQuestion>> GetActiveQuestionsByTypeAsync(string questionType)
        {
            return await _context.ConversationQuestions
                .Where(q => q.IsActive && q.QuestionType == questionType)
                .ToListAsync();
        }
    }
}
