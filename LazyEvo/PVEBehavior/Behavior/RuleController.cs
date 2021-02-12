namespace LazyEvo.PVEBehavior.Behavior
{
    using System;
    using System.Collections.Generic;

    internal class RuleController
    {
        private readonly List<Rule> _rules = new List<Rule>();

        public void AddRule(Rule rule)
        {
            lock (this._rules)
            {
                this._rules.Add(rule);
            }
        }

        public List<Rule> GetRules =>
            this._rules;
    }
}

