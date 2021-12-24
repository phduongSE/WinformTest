using WinformTester.Models;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace WinformTester
{
    public partial class MainForm : Form
    {
        private ConcurrentBag<Person> People = new ConcurrentBag<Person>();
        private Thread _thread;

        public MainForm()
        {
            InitializeComponent();
        }

        public void SetLabel(string data)
        {
            lbTime.Text = data;
        }

        private bool InitPeople(int quantity)
        {
            People.Clear();
            pb1.Value = 0;
            var onePercent = quantity / 100;
            if (onePercent <= 0)
                onePercent = 1;
            var result = Parallel.For(0, quantity, (i, state) =>
            {
                var person = new Person(i);
                People.Add(person);
                if (person.Id % onePercent == 0)
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        if (pb1.Value < 100)
                            pb1.Value += 1;
                    }));
            });
            return result.IsCompleted;
        }

        private void btnToUpper_Click(object sender, EventArgs e)
        {
            pb1.Value = 0;
            Thread trd = new Thread(new ThreadStart(this.UpperPeopleParallel));
            trd.IsBackground = true;
            trd.Start();
        }

        private void UpperPeople()
        {
            pb1.Value = 0;
            var onePercent = People.Count / 100;
            if (onePercent <= 0)
                onePercent = 1;
            var sw = Stopwatch.StartNew();
            foreach (var person in People)
            {
                person.ToUpperString();
                if (person.Id % onePercent == 0)
                    if (pb1.Value < 100)
                        pb1.Value += 1;
            }
            pb1.Value = 100;
            sw.Stop();
            lbTime.Text = $"Time: {sw.ElapsedMilliseconds} ms";
            dtgMain.Refresh();
        }

        private void UpperPeopleParallel()
        {
            var onePercent = People.Count / 100;
            if (onePercent <= 0)
                onePercent = 1;
            Task.Factory.StartNew(() =>
            {
                var sw = Stopwatch.StartNew();
                var result = Parallel.ForEach(People, person =>
                {
                    person.ToUpperString();
                    if (person.Id % onePercent == 0)
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            if (pb1.Value < 100)
                                pb1.Value += 1;
                        }));
                });

                if (result.IsCompleted)
                {
                    sw.Stop();
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        lbTime.Text = $"Time: {sw.ElapsedMilliseconds} ms";
                        dtgMain.Refresh();
                        pb1.Value = 100;
                    }));
                }
            });
        }

        private void btnLower_Click(object sender, EventArgs e)
        {
            pb1.Value = 0;
            var onePercent = People.Count / 100;
            if (onePercent <= 0)
                onePercent = 1;
            Task.Factory.StartNew(() =>
            {
                var sw = Stopwatch.StartNew();
                var result = Parallel.ForEach(People, person =>
                {
                    person.ToLowerString();
                    if (person.Id % onePercent == 0)
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            if (pb1.Value < 100)
                                pb1.Value += 1;
                        }));
                });

                if (result.IsCompleted)
                {
                    sw.Stop();
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        lbTime.Text = $"Time: {sw.ElapsedMilliseconds} ms";
                        dtgMain.Refresh();
                        pb1.Value = 100;
                    }));
                }
            });
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text != null)
            {
                pb1.Value = 0;
                var q = int.Parse(txtQuantity.Text);
                Task.Factory.StartNew(() =>
                {
                    var sw = Stopwatch.StartNew();
                    bool completed = InitPeople(q);
                    var ordered = People.OrderBy(x => x.Id).ToList();
                    sw.Stop();
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        dtgMain.DataSource = ordered;
                        lbTime.Text = $"Time: {sw.ElapsedMilliseconds} ms";
                        pb1.Value = 100;
                    }));
                });
            }
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            UpperPeople();
        }
    }
}