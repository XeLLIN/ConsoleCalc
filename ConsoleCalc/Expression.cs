using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleCalc
{
    class Expression
    {
        private Expression SubExpressionLeft;
        private Expression SubExpressionRight;
        private double valueOfExpression;
        private static Dictionary<char, int> operators = new Dictionary<char, int>()
        {
            {'+', 3},
            {'-', 3},
            {'*', 2},
            {'/', 2},
            {'^', 1}
        };
        private string currentOperator = "";
        public Expression(string expression)
        {
            if (expression.Contains("(") && expression.Contains(")"))
            {
                expression = DeleteOnlySideBrackets(expression);
            }
            if (operators.ContainsKey(expression[0]))
            {
                expression = "0" + expression;
            }
            if (!HasSubExpressinon(expression))
            {
                valueOfExpression = 0;
                if (!Double.TryParse(expression, out valueOfExpression))
                {
                    Console.WriteLine("Что-то пошло не так, ответ неверен, провертье правильность ввода!");
                }
            }
        }
        private bool HasSubExpressinon(string expression)
        {

            int operatorIndex = GetHigherPriority(expression);
            if (operatorIndex == -1)
            {
                return false;
            }
            else
            {
                ToSubExpressions(expression, operatorIndex);
                return true;
            }
        }
        private int GetHigherPriority(string expression)
        {
            int priority = 0, highPriorityOperatorIndex = 0;
            for (int index = expression.Length - 1; index > 0; index--)
            {
                if (operators.ContainsKey(expression[index]))
                {
                    if (IsBracketsBalanced(expression.Substring(0, index)))
                    {
                        if (operators[expression[index]] > priority)
                        {
                            priority = operators[expression[index]];
                            highPriorityOperatorIndex = index;
                        }
                    }
                }
            }
            if (priority == 0)
            {
                return -1;
            }
            else
            {
                return highPriorityOperatorIndex;
            }
        }

        private void ToSubExpressions(string expression, int operatorIndex)
        {
            SubExpressionLeft = new Expression(expression.Substring(0, operatorIndex));
            SubExpressionRight = new Expression(expression.Substring(operatorIndex + 1, expression.Length - operatorIndex - 1));
            currentOperator = expression[operatorIndex].ToString();
        }
        private bool IsBracketsBalanced(string expression)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char s in expression)
            {
                if (s == '(')
                {
                    stack.Push(s);
                }
                else if (s == ')')
                {
                    if (stack.Count == 0 || stack.Peek() != '(')
                    {
                        return false;
                    }
                    else
                    {
                        stack.Pop();
                    }
                }
            }
            return stack.Count == 0;
        }
        private string DeleteOnlySideBrackets(string expression)
        {
            string newExpression = null;
            if (expression.Length > 2)
            {
                if (expression[0] == '(' && expression[expression.Length - 1] == ')')
                {
                    newExpression = expression.Substring(1, expression.Length - 2);
                    if (IsBracketsBalanced(newExpression))
                    {
                        return newExpression;
                    }
                }
            }
            return expression;
        }
        public double FindSolve()
        {
            if (SubExpressionLeft == null && SubExpressionRight == null)
            {
                return valueOfExpression;
            }
            else
            {
                switch (currentOperator)
                {
                    case "+":
                        return SubExpressionLeft.FindSolve() + SubExpressionRight.FindSolve();
                    case "-":
                        return SubExpressionLeft.FindSolve() - SubExpressionRight.FindSolve();
                    case "*":
                        return SubExpressionLeft.FindSolve() * SubExpressionRight.FindSolve();
                    case "/":
                        return SubExpressionLeft.FindSolve() / SubExpressionRight.FindSolve();
                    case "^":
                        return Math.Pow(SubExpressionLeft.FindSolve(), SubExpressionRight.FindSolve());
                }
                return 0;
            }
        }
    }
}
