/**************************************************************************
 *                                                                        *
 *  File:        TuneWave.cs                                              *
 *  Copyright:   (c) 2023, Rareș-Andrei Dancău                            *
 *  Description: Clasa ce implementeaza un mp3 player                     *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/
using AxWMPLib;
using StateChange;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectIP
{
    /// <summary>
    /// Clasa principala a programului, ce extinde clasa Form
    /// </summary>
    public partial class TuneWave : Form
    {
        /// <summary>
        /// Contextul folosit de program pentru a schimba controalele din groupBox
        /// </summary>
        private readonly Context _context;
        /// <summary>
        /// Constructor ce initializeaza componentele Form-ului si apeleaza contructorul contextului
        /// </summary>
        public TuneWave()
        {
            InitializeComponent();
            _context = new Context();

        }
        /// <summary>
        /// Functie apelata atunci cand se apasa "Deschidere fisier"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeschidereFisierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Deschid un dialog si citesc fisierul dat de la utilizator. Daca nu am fisier valid, parasesc functia
                openFileDialog.Filter = "Audio file(*.mp3)|*.mp3";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                //Daca starea anterioara a fost deja SingleFileState, doar actualizez fisierul redat
                if (_context.StateNumber == MP3PlayerStates.SingleFileState)
                {
                    ((AxWindowsMediaPlayer)_context.Controls[0]).URL = Path.GetFullPath(openFileDialog.FileName);
                }
                //Altfel, schimb starea in SingleFileState si adaug controlul pe interfata
                else
                {
                    //Elimin ce era pe vechea interfata, daca era un player pornit il opresc si creez un nou player pe care il
                    //setez sa redea in bucla, cu dimensiunea si la locatia mentionate, si ii dau fisierul pe care sa-l redea
                    groupBox.Controls.Clear();
                    if (_context.StateNumber == MP3PlayerStates.PlaylistState || _context.StateNumber == MP3PlayerStates.RadioState)
                        ((AxWindowsMediaPlayer)_context.Controls[0]).Ctlcontrols.stop();
                    _context.StateNumber = MP3PlayerStates.SingleFileState;
                    _context.Request();
                    groupBox.Controls.Add(_context.Controls[0]);
                    _context.Controls[0].CreateControl();
                    ((AxWindowsMediaPlayer)_context.Controls[0]).settings.setMode("loop", true);
                    _context.Controls[0].Location = new System.Drawing.Point(6, 27);
                    _context.Controls[0].Size = new System.Drawing.Size(498, 368);
                    ((AxWindowsMediaPlayer)_context.Controls[0]).URL = Path.GetFullPath(openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand se apasa "Iesire"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Apelez functia standard de iesire din program
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata cand se apasa "Deschidere playlist"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeschiderePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Cer utilizatorului sa deschida un playlist existent. Daca nu deschide un fisier valid, parasesc functia.
                openFileDialog.Filter = "Playlist(*.txt)|*.txt";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                List<string> melodii = new List<string>();
                StreamReader sr = new StreamReader(Path.GetFullPath(openFileDialog.FileName));
                string[] lvls = sr.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                sr.Close();
                for (int i = 0; i < lvls.Length; i++)
                    melodii.Add(lvls[i]);
                // Crearea listei cu perechi cheie-valoare (calea completa la fisier - numele fisierului)
                var files = melodii.Select(path => new { Path = path, FileName = Path.GetFileName(path) }).ToList();
                //Daca starea anterioara a fost PlaylistState, doar reactualizez lista si redau noul prim fisier al playlist-ului.
                if (_context.StateNumber == MP3PlayerStates.PlaylistState)
                {
                    // Setarea DataSource-ului pentru ListBox
                    ((ListBox)_context.Controls[1]).DataSource = files;

                    // Setarea DisplayMember-ului si ValueMember-ului pentru ListBox
                    ((ListBox)_context.Controls[1]).DisplayMember = "FileName";
                    ((ListBox)_context.Controls[1]).ValueMember = "Path";


                    ((ListBox)_context.Controls[1]).SelectedIndexChanged += ListBoxPlaylist_SelectedIndexChanged;
                    ((AxWindowsMediaPlayer)_context.Controls[0]).URL = ((dynamic)((ListBox)_context.Controls[1]).SelectedItem).Path;
                }
                //Altfel, refac controalele aferente acestei stari si le afisez in groupBox.
                else
                {
                    groupBox.Controls.Clear();
                    if (_context.StateNumber == MP3PlayerStates.SingleFileState || _context.StateNumber == MP3PlayerStates.RadioState)
                        ((AxWindowsMediaPlayer)_context.Controls[0]).Ctlcontrols.stop();
                    _context.StateNumber = MP3PlayerStates.PlaylistState;
                    _context.Request();
                    groupBox.Controls.Add(_context.Controls[0]);
                    groupBox.Controls.Add(_context.Controls[1]);
                    groupBox.Controls.Add(_context.Controls[2]);
                    groupBox.Controls.Add(_context.Controls[3]);
                    _context.Controls[0].Location = new System.Drawing.Point(6, 27);
                    _context.Controls[0].Size = new System.Drawing.Size(498, 368);
                    _context.Controls[1].Location = new System.Drawing.Point(531, 27);
                    _context.Controls[1].Size = new System.Drawing.Size(200, 368);
                    ((ListBox)_context.Controls[1]).HorizontalScrollbar = true;

                    // Setarea DataSource-ului pentru ListBox
                    ((ListBox)_context.Controls[1]).DataSource = files;

                    // Setarea DisplayMember-ului si ValueMember-ului pentru ListBox
                    ((ListBox)_context.Controls[1]).DisplayMember = "FileName";
                    ((ListBox)_context.Controls[1]).ValueMember = "Path";


                    ((ListBox)_context.Controls[1]).SelectedIndexChanged += ListBoxPlaylist_SelectedIndexChanged;
                    ((AxWindowsMediaPlayer)_context.Controls[0]).URL = ((dynamic)((ListBox)_context.Controls[1]).SelectedItem).Path;
                    ((AxWindowsMediaPlayer)_context.Controls[0]).PlayStateChange += AxWindowsMediaPlayer_PlayStateChange;
                    _context.Controls[2].Text = "Random";
                    _context.Controls[2].Size = new System.Drawing.Size(66, 17);
                    _context.Controls[2].Location = new System.Drawing.Point(740, 27);
                    _context.Controls[3].Text = "Loop";
                    ((CheckBox)_context.Controls[3]).CheckedChanged += PlaylistLoop_CheckedChanged;
                    _context.Controls[3].Size = new System.Drawing.Size(66, 17);
                    _context.Controls[3].Location = new System.Drawing.Point(740, 47);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand se apasa pe checkbox-ul Loop din playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlaylistLoop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox checkBox = (CheckBox)sender;
                //Verific daca acum a fost activat checkbox-ul Loop, si atunci activez optiunea loop a obiectului axWindowsMediaPlayer
                if (checkBox.Checked)
                {
                    ((AxWindowsMediaPlayer)_context.Controls[0]).settings.setMode("loop", true);
                }
                //Altfel, inseamna ca utilizatorul a deselectat optiunea de a reda in bucla melodia curenta, si atunci dezactivez optiunea
                else
                {
                    ((AxWindowsMediaPlayer)_context.Controls[0]).settings.setMode("loop", false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand se schimba melodia selectata din playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListBox listBox = (ListBox)sender;
                int selectedIndex = listBox.SelectedIndex;
                //Daca elementul selectat este valid, atunci il redau pe player
                if (listBox.SelectedIndex > -1)
                {
                    ((AxWindowsMediaPlayer)_context.Controls[0]).URL = ((dynamic)listBox.SelectedItem).Path;
                    ((AxWindowsMediaPlayer)_context.Controls[0]).Ctlcontrols.play();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand obiectul AxWindowsMediaPlayer isi schimba starea
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AxWindowsMediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            try
            {
                //e.newState==8 verifica daca am ajuns la capatul unei melodii. Daca am ajuns la capatul unei melodii si 
                //nu este selectat checkbox-ul Loop, atunci pornesc un timer. A fost nevoie de aceasta abordare pentru ca
                //obiectul axWindowsMediaPlayer nu reda noul continut decat dupa o perioada de timp asincrona.
                if (e.newState == 8 && ((CheckBox)_context.Controls[3]).Checked == false)
                {
                    timer.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand trece perioada unui timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                //Dezactivez timerul. Dupa cum am mentionat anterior, trebuie asteptat asincron o perioada de timp. Altfel
                //controlul axWindowsMediaPlayer nu ar fi redat continutul.
                //Daca am un singur fisier in playlist, il redau in bucla.
                timer.Enabled = false;
                if (((ListBox)_context.Controls[1]).Items.Count == 1)
                    ((AxWindowsMediaPlayer)_context.Controls[0]).URL = ((dynamic)((ListBox)_context.Controls[1]).SelectedItem).Path;
                else
                {
                    //Daca este selectat checkbox-ul Random, redau un alt element al listei, astfel incat sa nu fie egal cu cel curent.
                    if (((CheckBox)_context.Controls[2]).Checked)
                    {
                        Random rnd = new Random();
                        int nowPlayIndex = rnd.Next(((ListBox)_context.Controls[1]).Items.Count);
                        while (nowPlayIndex == ((ListBox)_context.Controls[1]).SelectedIndex)
                            nowPlayIndex = rnd.Next(((ListBox)_context.Controls[1]).Items.Count);
                        ((ListBox)_context.Controls[1]).SelectedIndex = nowPlayIndex;
                    }
                    //Daca nu este checked, atunci redau secvential si daca am ajuns la sfarsit, o iau de la inceput.
                    else
                    {
                        if (((ListBox)_context.Controls[1]).SelectedIndex != ((ListBox)_context.Controls[1]).Items.Count - 1)
                        {
                            ((ListBox)_context.Controls[1]).SelectedIndex = ((ListBox)_context.Controls[1]).SelectedIndex + 1;
                        }
                        else
                        {
                            ((ListBox)_context.Controls[1]).SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand se apasa pe "Creare playlist"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CrearePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Pentru aceasta stare, nu mai conteaza starea anterioara. Elimin toate elementele. Daca aveam player pornit, il opresc.
                //Adaug elementele la groupBox, dupa ce le-am creat in context si particularizat aici.
                groupBox.Controls.Clear();
                if (_context.StateNumber == MP3PlayerStates.SingleFileState || _context.StateNumber == MP3PlayerStates.PlaylistState || _context.StateNumber == MP3PlayerStates.RadioState)
                {
                    ((AxWindowsMediaPlayer)_context.Controls[0]).Ctlcontrols.stop();
                }
                _context.StateNumber = MP3PlayerStates.MakePlaylistState;
                _context.Request();
                _context.Controls[0].Text = "Adaugă fișier";
                _context.Controls[1].Text = "Salvează playlist";
                _context.Controls[2].Enabled = false;
                _context.Controls[0].Location = new System.Drawing.Point(30, 30);
                _context.Controls[1].Location = new System.Drawing.Point(120, 30);
                _context.Controls[2].Location = new System.Drawing.Point(30, 60);
                ((TextBox)_context.Controls[2]).Multiline = true;
                ((TextBox)_context.Controls[2]).Size = new System.Drawing.Size(770, 300);
                ((Button)_context.Controls[0]).Click += AddButtonClickMakePlaylist;
                ((Button)_context.Controls[1]).Click += SaveButtonClickAddPlaylist;
                groupBox.Controls.Add(_context.Controls[0]);
                groupBox.Controls.Add(_context.Controls[1]);
                groupBox.Controls.Add(_context.Controls[2]);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand se apasa pe butonul de salvare din creare playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButtonClickAddPlaylist(object sender, EventArgs e)
        {
            try
            {
                //Afisez utilizatorului meniul de salvare al fisierului text cu playlist-ul.
                //Scriu continutul fisierului, daca totul este ok, si elimin toate controalele de pe groupBox.
                saveFileDialog.Filter = "Playlist(*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(saveFileDialog.FileName, ((TextBox)_context.Controls[2]).Text);
                    groupBox.Controls.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand se apasa pe butonul de adaugare cantec din creare playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButtonClickMakePlaylist(object sender, EventArgs e)
        {
            try
            {
                //Cer utilizatorului sa selecteze un fisier mp3, si daca a selectat un fisier valid, adaug locatia la continutul textbox-ului
                openFileDialog.Filter = "Audio file(*.mp3)|*.mp3";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                _context.Controls[2].Text += Path.GetFullPath(openFileDialog.FileName) + "\r\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand se apasa pe butonul de adaugare cantec din modificare playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButtonClickEditPlaylist(object sender, EventArgs e)
        {
            try
            {
                //Cer utilizatorului un fisier mp3 pe care-l adaug la lista curenta.
                openFileDialog.Filter = "Audio file(*.mp3)|*.mp3";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                List<object> currentList = new List<object>();

                foreach (object item in ((ListBox)_context.Controls[2]).Items)
                {
                    currentList.Add(item);
                }
                var newItem = new { Path = openFileDialog.FileName, FileName = Path.GetFileName(openFileDialog.FileName) };
                if (!currentList.Contains(newItem))
                    currentList.Add(newItem);

                ((ListBox)_context.Controls[2]).DataSource = null;
                ((ListBox)_context.Controls[2]).DataSource = currentList;
                ((ListBox)_context.Controls[2]).DisplayMember = "FileName";
                ((ListBox)_context.Controls[2]).ValueMember = "Path";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand se apasa pe butonul de stergere a indexului curent din editare playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveButtonClickEditPlaylist(object sender, EventArgs e)
        {
            try
            {
                //Sterg indexul curent
                int selectedIndex = ((ListBox)_context.Controls[2]).SelectedIndex;

                List<object> currentList = new List<object>();

                foreach (object item in ((ListBox)_context.Controls[2]).Items)
                {
                    currentList.Add(item);
                }
                currentList.RemoveAt(selectedIndex);

                ((ListBox)_context.Controls[2]).DataSource = null;
                ((ListBox)_context.Controls[2]).DataSource = currentList;
                ((ListBox)_context.Controls[2]).DisplayMember = "FileName";
                ((ListBox)_context.Controls[2]).ValueMember = "Path";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand se apasa pe butonul de salvare playlist modificat din creare playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButtonClickEditPlaylist(object sender, EventArgs e)
        {
            try
            {
                //Salvez fisierul editat
                saveFileDialog.Filter = "Playlist(*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Parcurgerea elementelor din ListBox și scrierea lor în fișier
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (var item in ((ListBox)_context.Controls[2]).Items)
                        {
                            string path = ((dynamic)item).Path;
                            writer.WriteLine(path);
                        }
                    }
                    groupBox.Controls.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand se apasa pe meniul Modificare playlist existent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModificarePlaylistExistentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Indiferent de starea anterioara, elimin elementele din groupBox. Daca aveam player deschis, il opresc.
                //Schimb starea contextului si modific proprietatile noilor elemente, pe care le adaug la groupBox.
                groupBox.Controls.Clear();
                if (_context.StateNumber == MP3PlayerStates.SingleFileState || _context.StateNumber == MP3PlayerStates.PlaylistState || _context.StateNumber == MP3PlayerStates.RadioState)
                {
                    ((AxWindowsMediaPlayer)_context.Controls[0]).Ctlcontrols.stop();
                }
                _context.StateNumber = MP3PlayerStates.EditPlaylistState;
                _context.Request();
                _context.Controls[0].Text = "Adaugare fisier";
                _context.Controls[0].Location = new System.Drawing.Point(420, 27);
                _context.Controls[0].Width = 150;
                ((Button)_context.Controls[0]).Click += AddButtonClickEditPlaylist;
                _context.Controls[1].Text = "Stergere index selectat";
                _context.Controls[1].Width = 150;
                _context.Controls[1].Location = new System.Drawing.Point(420, 67);
                ((Button)_context.Controls[1]).Click += RemoveButtonClickEditPlaylist;
                openFileDialog.Filter = "Playlist(*.txt)|*.txt";
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                List<string> melodii = new List<string>();
                StreamReader sr = new StreamReader(Path.GetFullPath(openFileDialog.FileName));
                string[] lvls = sr.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                sr.Close();
                for (int i = 0; i < lvls.Length; i++)
                    melodii.Add(lvls[i]);
                var files = melodii.Select(path => new { Path = path, FileName = Path.GetFileName(path) }).ToList();
                // Setarea DataSource-ului pentru ListBox
                ((ListBox)_context.Controls[2]).DataSource = files;

                // Setarea DisplayMember-ului si ValueMember-ului pentru ListBox
                ((ListBox)_context.Controls[2]).DisplayMember = "FileName";
                ((ListBox)_context.Controls[2]).ValueMember = "Path";
                _context.Controls[2].Size = new System.Drawing.Size(380, 368);
                _context.Controls[2].Location = new System.Drawing.Point(20, 27);

                _context.Controls[3].Text = "Salveaza playlist modificat";
                _context.Controls[3].Width = 150;
                _context.Controls[3].Location = new System.Drawing.Point(420, 107);
                ((Button)_context.Controls[3]).Click += SaveButtonClickEditPlaylist;
                groupBox.Controls.Add(_context.Controls[0]);
                groupBox.Controls.Add(_context.Controls[1]);
                groupBox.Controls.Add(_context.Controls[2]);
                groupBox.Controls.Add(_context.Controls[3]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand se modifica indexul selectat din listbox de la radio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxRadio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ListBox listBox = (ListBox)sender;
                int selectedIndex = listBox.SelectedIndex;
                //Daca s-a schimbat indexul selectat, si acesta este valid, modific postul radio redat de axWindowsMediaPlayer
                if (listBox.SelectedIndex > -1)
                {
                    ((AxWindowsMediaPlayer)_context.Controls[0]).URL = ((dynamic)listBox.SelectedItem).Url;
                    ((AxWindowsMediaPlayer)_context.Controls[0]).Ctlcontrols.play();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand se apasa pe meniul de ascultare post radio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AscultarePostRadioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Elimin elementele din groupBox. Daca aveam player deschis, il opresc.
                //Adaug noile elemente in context, apeland functia Request si le modific proprietatile aici.

                groupBox.Controls.Clear();
                if (_context.StateNumber == MP3PlayerStates.SingleFileState || _context.StateNumber == MP3PlayerStates.PlaylistState || _context.StateNumber == MP3PlayerStates.RadioState)
                {
                    ((AxWindowsMediaPlayer)_context.Controls[0]).Ctlcontrols.stop();
                }
                _context.StateNumber = MP3PlayerStates.RadioState;
                _context.Request();
                groupBox.Controls.Add(_context.Controls[0]);
                groupBox.Controls.Add(_context.Controls[1]);
                _context.Controls[0].Location = new System.Drawing.Point(6, 27);
                _context.Controls[0].Size = new System.Drawing.Size(498, 368);
                _context.Controls[1].Location = new System.Drawing.Point(531, 27);
                _context.Controls[1].Size = new System.Drawing.Size(270, 368);
                _context.Controls[1].Size = new System.Drawing.Size(270, 368);
                var radioStations = new List<object>
                {
                    new { Name = "Europa FM", Url = "https://astreaming.edi.ro:8443/EuropaFM_aac" },
                    new { Name = "Kiss FM", Url = "https://live.kissfm.ro:8443/kissfm.aacp" },
                    new { Name = "Magic FM", Url = "https://live.magicfm.ro:8443/magicfm.aacp" },
                    new { Name = "Pro FM", Url = "https://edge126.rcs-rds.ro/profm/profm.mp3" },
                    new { Name = "Radio ZU", Url = "http://zuicast.digitalag.ro:9420/zu" },
                    new { Name = "Digi FM", Url = "http://edge76.rdsnet.ro:84/digifm/digifm.mp3" },
                    new { Name = "Virgin Radio", Url = "https://astreaming.edi.ro:8443/VirginRadio_aac" }
                };

                var listBox = (ListBox)_context.Controls[1];
                listBox.DataSource = radioStations;
                listBox.DisplayMember = "Name";
                ((AxWindowsMediaPlayer)_context.Controls[0]).URL = "https://astreaming.edi.ro:8443/EuropaFM_aac";
                listBox.SelectedIndexChanged += ListBoxRadio_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Functie apelata atunci cand se apasa pe meniul ajutor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Deschid fisierul help daca s-a apasat in meniul ajutor
                System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + @"\TuneWave.chm");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
